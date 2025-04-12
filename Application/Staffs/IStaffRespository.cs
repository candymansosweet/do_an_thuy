using Application.Staffs.Request;
using Common.Models;
using Domain.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Staffs
{
    public interface IStaffRespository
    {
        public PaginatedList<Staff> Filter(FilterStaffRequest request);
        public Staff GetDetailById(Guid request);
        public Staff Create(CreateStaffRequest staff);
        public Staff Update(UpdateStaffRequest staff);
        public Staff DeleteById(Guid staffId);

    }
}
