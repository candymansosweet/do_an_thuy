using Application.Common.UserService;
using Application.Projects.Request;
using Application.Projects.Response;
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
        private readonly IUserService _userService;
        public ProjectRespository(ApplicationContext applicationContext, IMapper mapper, IUserService userService) : base(applicationContext, mapper) {
            _mapper = mapper;
            _userService = userService;
        }

        public ProjectResponse Create(CreateProjectRequest createProjectRequest)
        {
            var project = _mapper.Map<Project>(createProjectRequest);
            project.ProjectMems = createProjectRequest.ProjectMemIds.Select(
                id => new ProjectMem { 
                    ProjectId = project.Id, 
                    StaffId = id,
                    AppointedById = _userService.GetUserIdOnline()
                }).ToList();
            Add(project);
            return _mapper.Map<ProjectResponse>(project);
        }

        public ProjectResponse GetDetailById(Guid id)
        {
            Project? project = GetQueryable()
                .Include(p => p.ProjectMems)
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
            Delete(project);
            return _mapper.Map<ProjectResponse>(project);
        }

        public PaginatedList<ProjectResponse> Filter(FilterProjectRequest request)
        {
            IQueryable<Project> query = GetQueryable()
                .Include(p => p.ProjectMems)
                    .ThenInclude(pm => pm.Staff)
                .Include(p => p.Manager)
                .OrderBy(p => p.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var projects = query.ToList();
            var responses = _mapper.Map<List<ProjectResponse>>(projects);
            
            return new PaginatedList<ProjectResponse>(responses, CountTotal(), request.PageNumber, request.PageSize);
        }

        public ProjectResponse Edit(UpdateProjectRequest request)
        {
                var project = GetQueryable()
                    .Include(p => p.ProjectMems.Where(en => en.IsDeleted != true))
                    .FirstOrDefault(p => p.Id == request.Id);

                if (project == null)
                    throw new AppException(ExceptionCode.Notfound, "Project not found");

                // Update các trường khác
                _mapper.Map(request, project);
            project.ProjectMems.Add(new ProjectMem
            {
                ProjectId = project.Id,
                StaffId = request.ProjectMemIds[0],
                AppointedById = _userService.GetUserIdOnline()
            });
            foreach (var item in project.ProjectMems)
            {
                item.IsDeleted = true;
                _context.Entry(item).State = EntityState.Added; // Expected: Modified
            }
            foreach (var item in project.ProjectMems)
            {
            }
            //var newIds = request.ProjectMemIds.ToHashSet();
            //var existing = project.ProjectMems.ToList();
            //var existingIds = existing.Select(pm => pm.StaffId).ToHashSet();

            //var userId = _userService.GetUserIdOnline();

            //// Xóa ProjectMems cũ nếu không còn trong request
            //foreach (var oldPm in existing.Where(pm => !newIds.Contains(pm.StaffId)).ToList())
            //{
            //    _context.ProjectMems.Remove(oldPm);
            //}

            //// Thêm mới những ProjectMem chưa có
            //foreach (var newId in newIds.Except(existingIds))
            //{
            //_context.ProjectMems.Add(new ProjectMem
            //    {
            //        ProjectId = project.Id,
            //        StaffId = newId,
            //        AppointedById = userId
            //    });
            //}

            Update(project); // Chỉ cập nhật project chính

            //Update(project);
            return _mapper.Map<ProjectResponse>(project);
        }

    }
} 