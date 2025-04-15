using Application.Accounts.Request;
using Application.Authenticates;
using Application.Staffs;
using Application.Staffs.Request;
using Common.Exceptions;
using Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatesController : ControllerBase
    {
        private readonly IAuthenticateRespository _authenticateRespository;
        private readonly AppSettings _appSettings;
        public AuthenticatesController(
            IAuthenticateRespository authenticateRespository,
            IOptions<AppSettings> appSettings
            )
        {
            _authenticateRespository = authenticateRespository;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _authenticateRespository.Authenticate(request, _appSettings.Secret));
            }
            catch (AppException)
            {
                throw; // Rethrow AppException to be handled by ErrorHandlerMiddleware
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        // [HttpGet("logout")]
        // public IActionResult Filter()
        // {
        //     try
        //     {
        //         return StatusCode(StatusCodes.Status200OK, _authenticateRespository.Logout());
        //     }
        //     catch (AppException)
        //     {
        //         throw; // Rethrow AppException to be handled by ErrorHandlerMiddleware
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //     }
        // }
    }
}
