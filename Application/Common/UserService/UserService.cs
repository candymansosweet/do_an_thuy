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

    }
    public class UserService: IUserService
    {
        //private readonly AppSettings _appSettings;
        // private IHttpContextAccessor _httpContextAccessor;

        //IOptions<AppSettings> appSettings,
        //IMemoryCache cache,
        // public UserService(IHttpContextAccessor httpContextAccessor)
        // {
        //    //_cache = cache;
        //    _httpContextAccessor = httpContextAccessor;
        // }
        public Guid GetUserIdOnline()
        {
            return new Guid("765555A5-A970-45C2-8DE5-990ADE6B9602");
            //return (string)_httpContextAccessor.HttpContext.Items["UserId"];
        }
        public string GetUserNameOnline()
        {
            return "Admin";
            //string Username = String.Empty;
            //try
            //{
            //    Username = (string)_httpContextAccessor.HttpContext.Items["Username"];
            //}
            //catch (Exception ex)
            //{
            //}
            //return Username;
        }
    }
}
