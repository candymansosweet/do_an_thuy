using Application.Accounts;
using Application.Accounts.Request;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Filter([FromQuery] FilterAccountRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _accountRepository.Filter(request));
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

        [HttpGet("{id}")]
        public IActionResult GetDetailById(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _accountRepository.GetDetailById(id));
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateAccountRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _accountRepository.Create(request));
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

        [HttpPut]
        public IActionResult Update([FromBody] UpdateAccountRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _accountRepository.Update(request));
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

        // [HttpDelete("{id}")]
        // public IActionResult Delete(Guid id)
        // {
        //     try
        //     {
        //         return StatusCode(StatusCodes.Status200OK, _accountRepository.DeleteById(id));
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