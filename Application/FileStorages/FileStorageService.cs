using Application.FileStorages.Request;
using Application.PathServices.Requests;
using Application.PathServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.FileStorages.Response;

namespace Application.FileStorages
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IPathService _pathService;

        public FileStorageService(IPathService pathService)
        {
            _pathService = pathService;
        }

        public async Task<FileUploadResponse> UploadFileAsync(FileUploadRequest fileUploadDto)
        {
            // lấy đường dẫn
            var av = Directory.GetCurrentDirectory();
            PathDto path = await _pathService.CreatePath();
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), path.Url.Replace($"/", "\\"));
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // Chỉ tạo thư mục
            }
            var filePath = Path.Combine(folderPath, fileUploadDto.file.FileName);
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileUploadDto.file.CopyToAsync(file);
            }
            var rs = new FileUploadResponse()
            {
                FileName = fileUploadDto.file.FileName,
                FilePath = path.Url.ToString() + '\\'+ fileUploadDto.file.FileName.ToString(),
                FileType = fileUploadDto.file.ContentType,
                FileSize = fileUploadDto.file.Length
            };
            return rs;
        }
        // Path.Combine(filePath.Replace($"/", "\\")); // Trả về đường dẫn local
        public System.Threading.Tasks.Task DeleteFileAsync(string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl.Replace($"/", "\\"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public Task<Stream> DownloadFileAsync(string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl.Replace($"/", "\\"));
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return System.Threading.Tasks.Task.FromResult((Stream)stream);
        }

    }
}
