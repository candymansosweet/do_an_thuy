using Common.Constants;
using Common.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.UserService
{
    public interface IUserService
    {
        Guid GetUserIdOnline();
        string GetUserNameOnline();
        UserInfor GetUserInfor(Guid accId);

    }
    public class UserService : IUserService
    {
        //private readonly AppSettings _appSettings;
        private IHttpContextAccessor _httpContextAccessor;

        //IOptions<AppSettings> appSettings,
        //IMemoryCache cache,
        // public UserService()
        // {
        //    //_cache = cache;
        //    
        // }
        protected readonly ApplicationContext _context;
        public UserService(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserIdOnline()
        {
            if (_httpContextAccessor?.HttpContext?.Items == null)
            {
                throw new InvalidOperationException("HttpContext is not available");
            }
            return (Guid)_httpContextAccessor.HttpContext.Items["UserId"];
        }
        public string GetUserNameOnline()
        {
            if (_httpContextAccessor?.HttpContext?.Items == null)
            {
                return string.Empty;
            }
            try
            {
                return (string)_httpContextAccessor.HttpContext.Items[ContextItems.Username] ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public string GetUserFullNameOnline()
        {
            if (_httpContextAccessor?.HttpContext?.Items == null)
            {
                return string.Empty;
            }
            try
            {
                return (string)_httpContextAccessor.HttpContext.Items[ContextItems.StaffName] ?? string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public UserInfor GetUserInfor(Guid accId)
        {
            Account acc = _context.Accounts.First(en => en.Id == accId);
            Staff staff = _context.Staffs.First(en => en.Id == acc.StaffId);
            return new UserInfor()
            {
                UserId = accId,
                Username = acc.Name,
                Role = acc.Role,
                StaffId = staff.Id,
                StaffName = staff.FullName
            };
        }
    }
}
