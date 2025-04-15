using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class ProjectMem: BaseModel
    {
        // Dự án
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project? Project { get; set; }

        // Nhân sự tham gia dự án
        public Guid StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public virtual Staff? Staff { get; set; }

        // Ngày tham gia
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        // Người bổ nhiệm
        public Guid AppointedById { get; set; }
    }
} 