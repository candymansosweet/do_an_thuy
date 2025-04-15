using Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileStorages.Request
{
    public class FileUploadRequest
    {
        [FromForm]
        public IFormFile file { get; set; }
    }
    public class FilesUploadRequest
    {
        [FromForm]
        public List<IFormFile> files { get; set; }
    }
}
