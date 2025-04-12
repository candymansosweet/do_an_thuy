using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Projects.Request
{
    public class UpdateProjectRequest : IMapTo<Project>
    {
        public Guid Id { get; set; } // mã dự án
        public string ProjectName { get; set; } // tên dự án
        public Guid ManagerId { get; set; } // người quản lý
        public string Description { get; set; } // mô tả dự án
        public DateTime DeadlineDate { get; set; } // ngày hết hạn
        public List<Guid> ProjectMemIds { get; set; } // danh sách nhân sự tham gia dự án
    }
} 