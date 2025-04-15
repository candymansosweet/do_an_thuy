using Common.Constants;
using Common.Models;
using Common.Services.JwtTokenService;
using Microsoft.Extensions.Options;
using Web.API.Services;

namespace Web.API.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context, IJwtTokenService jwtTokenService, IUserService userService)
        {
            string? token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            Guid? userId = jwtTokenService.ValidateToken(token, _appSettings.Secret);
            if (userId != null)
            {
                var userInfor = userService.GetUserInfor((Guid)userId);
                // attach user to context on successful jwt validation
                context.Items[ContextItems.UserId] = userInfor.UserId;
                context.Items[ContextItems.Username] = userInfor.Username;
                context.Items[ContextItems.StaffId] = userInfor.StaffId;
                context.Items[ContextItems.StaffName] = userInfor.StaffName;
                context.Items[ContextItems.Role] = userInfor.Role;
            }
            await _next(context);
        }
    }
}
