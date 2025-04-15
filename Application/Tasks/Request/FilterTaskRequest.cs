using System;
using System.Collections.Generic;
using Application.Common.Mapping;
using Application.Tasks.Response;
using Domain.Entities;

namespace Application.Tasks.Request
{
    public class FilterTaskRequest: BasePaginatedQuery, IMapFrom<TaskResponse>
    {
        public Guid? ProjectId { get; set; } = null;
        public Domain.Entities.TaskStatus? Status { get; set; } = null;
        public Guid? CurrentUserAssignId { get; set; } = null;
        public string? Name { get; set; } = null;
    }
} 