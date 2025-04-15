using Application.Accounts;
using Application.Accounts.Request;
using Application.FileStorages;
using Application.FileStorages.Request;
using Application.FileStorages.Response;
using Application.Tasks;
using Common.Constants;
using Common.Exceptions;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ApplicationContext _context;

        public UploadsController(
            IFileStorageService fileStorageService,
            ApplicationContext context
            )
        {
            _fileStorageService = fileStorageService;
            _context = context;
        }
        [HttpPost("Upload/Task/{taskId}")]
        public async Task<IActionResult> UploadFile(Guid taskId, [FromForm] FilesUploadRequest fileUpload)
        {
            foreach (var item in fileUpload.files)
            {
                using (var stream = item.OpenReadStream())
                {
                    var fileUploadDto = new FileUploadRequest()
                    {
                        file = item
                    };
                    FileUploadResponse dto = await _fileStorageService.UploadFileAsync(fileUploadDto);
                    TaskFile taskFile = new TaskFile()
                    {
                        TaskId = taskId,
                        FileName = dto.FileName,
                        FilePath = dto.FilePath,
                        FileType = dto.FileType,
                        FileSize = dto.FileSize,
                    };
                    _context.TaskFiles.Add(taskFile);
                }
                    _context.SaveChanges();
                
            }
            return Ok();
        }
        [HttpDelete("Upload/Task/{fileId}")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            TaskFile taskFile = _context.TaskFiles.First(x => x.Id == fileId);
            _context.TaskFiles.Remove(taskFile);
            _context.SaveChanges();
            return Ok();
        }

        // Download file
        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] string idOnServer)
        {
            return File(await _fileStorageService.DownloadFileAsync(idOnServer), "application/octet-stream");
        }
    }
} 