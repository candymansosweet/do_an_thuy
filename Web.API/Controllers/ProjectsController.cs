using Application.Projects;
using Application.Projects.Request;
using Application.Tasks.Request;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Services;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRespository _projectRespository;
        private readonly IUserService _userService;
        public ProjectsController(
            IUserService userService,
            IProjectRespository projectRespository)
        {
            _projectRespository = projectRespository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Filter([FromQuery] FilterProjectRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _projectRespository.Filter(request));
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
        [HttpPost("GetStatusTasks")]
        public IActionResult GetStatusTasks([FromBody] GetStatusTasksProjectRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _projectRespository.GetStatusTasks(request.Projectid));
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
        public IActionResult Add([FromBody] CreateProjectRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _projectRespository.Create(request, _userService.GetStaffIdOnline()));
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
        public IActionResult GetDetailById(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _projectRespository.GetDetailById(id));
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
        public IActionResult Update([FromBody] UpdateProjectRequest request)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _projectRespository.Edit(request, _userService.GetStaffIdOnline()));
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
                return StatusCode(StatusCodes.Status200OK, _projectRespository.DeleteById(id));
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