using Application.Common.Mapping;
using Application.Projects.Response;
using Common.Models;

namespace Application.Projects.Request
{
    public class FilterProjectRequest: BasePaginatedQuery, IMapFrom<ProjectResponse>
    {
    }
} 