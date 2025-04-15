using Application.Tasks.Request;
using Application.Tasks.Response;
using Domain.Entities;
using Infrastructure.Repository;
using Common.Models;

namespace Application.Tasks
{
    public interface ITaskRespository : IGenericRepository<Domain.Entities.Task>
    {
        TaskResponse Start(Guid request);
        TaskResponse Create(CreateTaskRequest request);
        TaskResponse Update(UpdateTaskRequest request);
        TaskResponse Delete(Guid id);
        TaskResponse GetById(Guid id);
        List<TaskResponse> GetAll();
        PaginatedList<TaskResponse> Filter(FilterTaskRequest request);
    }
} 