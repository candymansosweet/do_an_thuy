using Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Tasks.Request
{
    public class UpdateTaskRequest: IMapTo<Domain.Entities.Task>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public Domain.Entities.TaskStatus Status { get; set; }

        public Guid? CurrentUserAssignId { get; set; }
        public DateTime? DeadlineDate { get; set; }

        public string? Description { get; set; }

        public List<string> Files { get; set; } = new List<string>();
    }
} 