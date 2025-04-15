using Application.Common.Models;
using Application.PathServices.Requests;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unitilities.DateTimeUnitilities;

namespace Application.PathServices
{
    public class PathService : IPathService
    {
        private readonly string _uploadPath;
        public PathService(IOptions<FileSettings> options)
        {
            _uploadPath = options.Value.UploadPath;
        }
        public async Task<PathDto> CreatePath()
        {
            string[] dateTimeUpload = FormatToString.FormatToPathDeep("dd/MM/yyyy");
            return new PathDto()
            {
                Url = Path.Combine(_uploadPath, dateTimeUpload[2], dateTimeUpload[1], dateTimeUpload[0])
            };
        }
    }
}
