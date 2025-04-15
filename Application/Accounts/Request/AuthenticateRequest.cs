using Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Accounts.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
} 