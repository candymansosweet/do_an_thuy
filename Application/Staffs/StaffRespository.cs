using Application.Staffs.Request;
using AutoMapper;
using Common.Exceptions;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Staffs
{
    public class StaffRespository: BaseRepository<Staff>, IStaffRespository
    {
        public StaffRespository(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper) { }

        public Staff Add(CreateStaffRequest createStaffRequest)
        {
            var staff = _mapper.Map<Staff>(createStaffRequest);
            Add(staff);
            return staff;
        }
        public Staff DeleteById(Guid staffId)
        {
            Staff? staff = GetById(staffId);
            if (staff == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Staff not found");
            }
            Delete(staff);
            return staff;

        }
        public List<Staff> Filter()
        {
            return GetQueryable().ToList();
        }

        public Staff Update(UpdateStaffRequest staff)
        {
            Staff? staffCMD = GetById(staff.Id);
            if (staffCMD == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Staff not found");
            }
            _mapper.Map(staff, staffCMD);
            Update(staffCMD);
            return staffCMD;
        }
    }
}
