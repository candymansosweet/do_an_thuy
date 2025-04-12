using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Project: BaseModel
    {
        // Tên dự án
        public string ProjectName { get; set; }

        // Người quản lý dự án
        public Guid ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public Staff Manager { get; set; }

        // Mô tả dự án
        public string Description { get; set; }

        // Ngày hết hạn dự án
        public DateTime DeadlineDate { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public virtual List<ProjectMem> ProjectMems { get; set; } = new List<ProjectMem>();

    }
} 