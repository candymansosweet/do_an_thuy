using Common.Constants;
using System;

namespace Application.Accounts.Request
{
    public class UpdateAccountRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid StaffId { get; set; }
        public RoleValue.ROLE Role { get; set; }
    }
} 