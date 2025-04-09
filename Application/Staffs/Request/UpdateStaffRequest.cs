using Domain.Entities;
using Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Staffs.Request
{
    public class UpdateStaffRequest : IMapTo<Staff>
    {
        public Guid Id { get; set; } // mã nhân viên
        public string Code { get; set; } // mã nhân viên
        public string FullName { get; set; } // họ tên

        public Potition Potition { get; set; } // chức danh

        public Department Department { get; set; } // phòng ban

        public string Email { get; set; } // email
    }
}
