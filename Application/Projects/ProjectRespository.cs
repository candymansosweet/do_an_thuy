using Application.Projects.Request;
using Application.Projects.Response;
using Application.Tasks;
using AutoMapper;
using Common.Exceptions;
using Common.Models;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects
{
    public class ProjectRespository: BaseRepository<Project>, IProjectRespository
    {
        private readonly IMapper _mapper;
        private readonly ITaskRespository _taskRespository;
        public ProjectRespository(ApplicationContext applicationContext, IMapper mapper, ITaskRespository taskRespository) : base(applicationContext, mapper) {
            _mapper = mapper;
            _taskRespository = taskRespository;
        }

        public ProjectResponse Create(CreateProjectRequest createProjectRequest, Guid staffId)
        {
            var project = _mapper.Map<Project>(createProjectRequest);
            project.ProjectMems = createProjectRequest.ProjectMemIds.Select(
                id => new ProjectMem { 
                    ProjectId = project.Id, 
                    StaffId = id,
                    AppointedById = staffId
                }).ToList();
            Add(project);
            return _mapper.Map<ProjectResponse>(project);
        }

        public ProjectResponse GetDetailById(Guid id)
        {
            Project? project = GetQueryable()
                .Include(p => p.ProjectMems.Where(en => en.IsDeleted == false))
                    .ThenInclude(pm => pm.Staff)
                .Include(p => p.Manager)
                .FirstOrDefault(p => p.Id == id && !p.IsDeleted);

            if (project == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Project not found");
            }
            return _mapper.Map<ProjectResponse>(project);
        }

        public ProjectResponse DeleteById(Guid projectId)
        {
            Project? project = GetById(projectId);
            if (project == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Project not found");
            }
            var projectMems = _context.ProjectMems.Where(_ => _.ProjectId == projectId).ToList();
            if(projectMems.Any())
            {
                _context.ProjectMems.RemoveRange(projectMems);
                _context.SaveChanges();


            }
            project.DeleteMe();
            _context.Projects.Update(project);


            _context.SaveChanges();
            return _mapper.Map<ProjectResponse>(project);
        }

        public PaginatedList<ProjectResponse> Filter(FilterProjectRequest request)
        {
            IQueryable<Project> query = GetQueryable()
                .Include(p => p.ProjectMems.Where(en => en.IsDeleted == false))
                    .ThenInclude(pm => pm.Staff)
                .Include(p => p.Manager)
                .OrderByDescending(p => p.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var projects = query.ToList();
            var responses = _mapper.Map<List<ProjectResponse>>(projects);
            
            return new PaginatedList<ProjectResponse>(responses, CountTotal(), request.PageNumber, request.PageSize);
        }

        public ProjectResponse Edit(UpdateProjectRequest request, Guid staffId)
        {
            var project = GetQueryable()
                .Include(p => p.ProjectMems.Where(en => en.IsDeleted != true))
                .FirstOrDefault(p => p.Id == request.Id);

            if (project == null)
                throw new AppException(ExceptionCode.Notfound, "Project not found");

            // Update các trường khác
            _mapper.Map(request, project);

            var listCurrentMemId = project.ProjectMems.Select(en => en.StaffId).ToList();

            var projectMemDeleteIds = listCurrentMemId.Except(request.ProjectMemIds);
            var projectMemAddIds = request.ProjectMemIds.Except(listCurrentMemId);

            // Xóa các ProjectMem cũ
            foreach (var memId in projectMemDeleteIds){
                var mem = project.ProjectMems.First(en => en.StaffId == memId);
                mem.IsDeleted = true;
                _context.Entry(mem).State = EntityState.Modified;
            }

            // Thêm các ProjectMem mới
            foreach (var memId in projectMemAddIds){
                var mem = new ProjectMem(){
                    ProjectId = project.Id,
                    StaffId = memId,
                    AppointedById = staffId
                };
                project.ProjectMems.Add(mem);
                _context.Entry(mem).State = EntityState.Added;
            }       
            Update(project); // Chỉ cập nhật project chính

            //Update(project);
            return _mapper.Map<ProjectResponse>(project);
        }
        public GetStatusTasksResponse GetStatusTasks(string? projectId)
        {
            GetStatusTasksResponse response = new GetStatusTasksResponse();
            var now = DateTime.Now;
            var query =  _taskRespository.GetQueryable();
            if (!String.IsNullOrEmpty(projectId))
            {
                query = query.Where(en => en.ProjectId == Guid.Parse(projectId));
            }
            response.NotStarted = query.Count(en => en.Status == Domain.Entities.TaskStatus.NotStarted);
            response.InProgress = query.Count(
                en => en.Status == Domain.Entities.TaskStatus.InProgress
            );
            response.Completed = query.Count(en => en.Status == Domain.Entities.TaskStatus.Completed);
            response.Overdue = query.Count(
                en => en.DeadlineDate < now
            );
            return response;
        }
    }
} 