using Common.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account : BaseModel
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public Guid StaffId { get; set; }
        public RoleValue.ROLE Role { get; set; }
    }
}
