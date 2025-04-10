using Application.Staffs;
using Application.Staffs.Request;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRespository _staffRespository;
        public StaffsController(
            IStaffRespository staffRespository) : base(
            )
        {
            _staffRespository = staffRespository;
        }
        [HttpGet]
        public IActionResult Filter([FromQuery] FilterStaffRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _staffRespository.Filter(request));
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
        public IActionResult Add([FromBody] CreateStaffRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _staffRespository.Add(request));
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
        public IActionResult Update([FromBody] UpdateStaffRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _staffRespository.Update(request));
            }
            catch(AppException)
            {
                throw; // Rethrow AppException to be handled by ErrorHandlerMiddleware
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _staffRespository.DeleteById(id));
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
    }
}
