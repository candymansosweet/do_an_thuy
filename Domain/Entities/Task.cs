using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Task : BaseModel
    {
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public Guid? CurrentUserAssignId { get; set; }
        [ForeignKey("CurrentUserAssignId")]
        public virtual Staff? CurrentUserAssign { get; set; }
        public DateTime DeadlineDate { get; set; } = DateTime.Now;
        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public string? Description { get; set; }
                // Danh sách công việc mà nhân viên tham gia
        [JsonIgnore]
        public virtual List<TaskFile> TaskFiles { get; set; } = new List<TaskFile>();
    }

    public enum TaskStatus
    {
        NotStarted,     // Chưa làm
        InProgress,     // Đang làm
        Completed,      // Hoàn thành
        Overdue         // Quá hạn
    }
} 