using Application.PathServices.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PathServices
{
    public interface IPathService
    {
        public Task<PathDto> CreatePath();
    }
}
