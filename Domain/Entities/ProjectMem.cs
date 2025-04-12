using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProjectMem: BaseModel
    {
        // Dự án
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; }

        // Nhân sự tham gia dự án
        public Guid StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff Staff { get; set; }

        // Ngày tham gia
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        // Người bổ nhiệm
        public Guid? AppointedById { get; set; }

        [ForeignKey(nameof(AppointedById))]
        public Staff AppointedBy { get; set; }
    }
} 