using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Tasks.Request
{
    public class CreateTaskRequest : IMapTo<Domain.Entities.Task>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Guid? CurrentUserAssignId { get; set; }

        [Required]
        public DateTime DeadlineDate { get; set; }

        public string? Description { get; set; }

        public List<string> Files { get; set; } = new List<string>();
    }
} 