
using System.Text.Json.Serialization;


namespace Domain.Entities
{
    public class Staff : BaseModel
    {
        public string Code { get; set; } // mã nhân viên
        public string FullName { get; set; } // họ tên

        public Position Position { get; set; } // chức danh

        public Department Department { get; set; } // phòng ban

        public string Email { get; set; } // email

        // Danh sách dự án mà nhân viên tham gia
        [JsonIgnore]
        public virtual List<ProjectMem> ProjectMems { get; set; } = new List<ProjectMem>();

        // Danh sách công việc mà nhân viên tham gia
        [JsonIgnore]
        public virtual List<Task> Tasks { get; set; } = new List<Task>();
    }

    public enum Position
    {
        NhanVien,      // nhân viên
        QuanLy,        // quản lý
        LanhDao        // lãnh đạo
    }

    public enum Department
    {
        PhongKyThuat,                    // phòng kỹ thuật
        PhongTrienKhai,                  // phòng triển khai
        PhongKinhDoanh,                  // phòng kinh doanh
        PhongCoVan,                      // phòng cố vấn
        PhongHanhChinhNhanSuTongHop      // phòng hành chính nhân sự tổng hợp
    }
}
