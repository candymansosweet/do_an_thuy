using Application.Staffs.Request;
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
        public List<Staff> Filter();
        public Staff Add(CreateStaffRequest staff);
        public Staff Update(UpdateStaffRequest staff);
        public Staff DeleteById(Guid staffId);

    }
}
