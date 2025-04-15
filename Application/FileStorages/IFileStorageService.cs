using Application.FileStorages.Request;
using Application.FileStorages.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileStorages
{
    public interface IFileStorageService
    {
        Task<FileUploadResponse> UploadFileAsync(FileUploadRequest fileUploadDto);
        Task DeleteFileAsync(string fileUrlOrId);
        Task<Stream> DownloadFileAsync(string fileUrlOrId);
    }
}
