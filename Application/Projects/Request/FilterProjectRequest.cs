using Application.Common.Mapping;
using Application.Projects.Response;

namespace Application.Projects.Request
{
    public class FilterProjectRequest: BasePaginatedQuery, IMapFrom<ProjectResponse>
    {
    }
} 