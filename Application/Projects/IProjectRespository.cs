using Application.Projects.Request;
using Application.Projects.Response;
using Common.Models;
using Domain.Entities;

namespace Application.Projects
{
    public interface IProjectRespository
    {
        ProjectResponse Create(CreateProjectRequest createProjectRequest);
        ProjectResponse GetDetailById(Guid id);
        ProjectResponse DeleteById(Guid projectId);
        PaginatedList<ProjectResponse> Filter(FilterProjectRequest request);
        ProjectResponse Edit(UpdateProjectRequest request);
    }
} 