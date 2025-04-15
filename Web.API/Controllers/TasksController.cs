using Application.Tasks;
using Application.Tasks.Request;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Attributes;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRespository _taskRespository;
        public TasksController(
            ITaskRespository taskRespository)
        {
            _taskRespository = taskRespository;
        }
        [HttpGet]
        public IActionResult Filter([FromQuery] FilterTaskRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _taskRespository.Filter(request));
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateTaskRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _taskRespository.Create(request));
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _taskRespository.GetById(id));
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateTaskRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _taskRespository.Update(request));
            }
            catch (AppException)
            {
                throw;
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
                return StatusCode(StatusCodes.Status200OK, _taskRespository.Delete(id));
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("Start/{id}")]
        public IActionResult Start(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _taskRespository.Start(id));
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
} 