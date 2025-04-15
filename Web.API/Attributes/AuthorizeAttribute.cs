using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Common.Constants;

namespace Web.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        //private readonly Permissions[] _permissions;

        public AuthorizeAttribute() { }
        //public AuthorizeAttribute(Permissions[] permissions)
        //{
        //    _permissions = permissions;
        //}
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }
            // authorization
            var a = context.HttpContext.Items[ContextItems.UserId];
            Guid? userId = (Guid?)context.HttpContext.Items[ContextItems.UserId];
            if (context.HttpContext.Items[ContextItems.UserId] == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            //Lấy thông tin người dùng
            //Permissions[] permissions =  context.HttpContext.Items[ContextItems.Permissions]; 
        }
    }
}
