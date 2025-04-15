using Common.Constants;
using System;

namespace Application.Accounts.Request
{
    public class CreateAccountRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid StaffId { get; set; }
        public RoleValue.ROLE Role { get; set; }
    }
} 