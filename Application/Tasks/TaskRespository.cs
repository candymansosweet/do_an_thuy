using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repository;
using Application.Tasks.Request;
using Application.Tasks.Response;
using Microsoft.EntityFrameworkCore;
using Common.Exceptions;
using Common.Models;

namespace Application.Tasks
{
    public class TaskRespository : BaseRepository<Domain.Entities.Task>, ITaskRespository
    {

        public TaskRespository(

            ApplicationContext context, IMapper mapper
            ) 
            : base(context, mapper)
        {
        }

        public TaskResponse Create(CreateTaskRequest request)
        {
            Domain.Entities.Task task = _mapper.Map<Domain.Entities.Task>(request);
            task.Status = Domain.Entities.TaskStatus.NotStarted;
            Add(task);
            return _mapper.Map<TaskResponse>(task);
        }

        public TaskResponse Update(UpdateTaskRequest request)
        {
            var task = GetQueryable()
                .Include(t => t.CurrentUserAssign)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.Id == request.Id && !t.IsDeleted);

            if (task == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Task not found");
            }
            if (request.CurrentUserAssignId != null)
            {
                task.CurrentUserAssignId = request.CurrentUserAssignId;
            }
            if (request.Status != null)
            {
                task.Status = request.Status;
            }
            if (request.Name != null)
            {
                task.Name = request.Name;
            }

            Update(task);
            return _mapper.Map<TaskResponse>(task);
        }

        public TaskResponse Delete(Guid id)
        {
            var task = GetQueryable()
                .Include(t => t.CurrentUserAssign)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.Id == id && !t.IsDeleted);

            if (task == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Task not found");
            }

            Delete(task);
            return _mapper.Map<TaskResponse>(task);
        }

        public TaskResponse GetById(Guid id)
        {
            var task = GetQueryable()
                .Include(t => t.CurrentUserAssign)
                .Include(t => t.Project)
                .Include(t => t.TaskFiles.Where(en => en.IsDeleted != true))
                .FirstOrDefault(t => t.Id == id && !t.IsDeleted);

            if (task == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Task not found");
            }

            return _mapper.Map<TaskResponse>(task);
        }

        public List<TaskResponse> GetAll()
        {
            var tasks = GetQueryable()
                .Include(t => t.CurrentUserAssign)
                .Include(t => t.Project)
                .ToList();

            return _mapper.Map<List<TaskResponse>>(tasks);
        }

        public PaginatedList<TaskResponse> Filter(FilterTaskRequest request)
        {
            var now = DateTime.Now;
            IQueryable<Domain.Entities.Task> query = GetQueryable()
                .Include(t => t.CurrentUserAssign)
                .Include(t => t.Project);
            if(request.ProjectId != null)
            {
                query = query.Where(en =>en.ProjectId == request.ProjectId);
            }
            if (request.CurrentUserAssignId != null)
            {
                query = query.Where(en => en.CurrentUserAssignId == request.CurrentUserAssignId);
            }
            if (request.Status != null)
            {
                query = query.Where(en => en.Status == request.Status);
            }
            if (request.Name != null)
            {
                query = query.Where(en => en.Name.Contains(request.Name));
            }
            var tasks = query.OrderByDescending(t => t.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
            var responses = _mapper.Map<List<TaskResponse>>(tasks);
            
            return new PaginatedList<TaskResponse>(responses, CountTotal(), request.PageNumber, request.PageSize);
        }

        public TaskResponse Start(Guid id)
        {
            var task = GetQueryable()
                .FirstOrDefault(t => t.Id == id && !t.IsDeleted);

            if (task == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Task not found");
            }
            task.Status = Domain.Entities.TaskStatus.InProgress;

            Update(task);
            return _mapper.Map<TaskResponse>(task);
        }


    }
} 