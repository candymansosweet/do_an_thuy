using Application.Staffs.Request;
using AutoMapper;
using Azure.Core;
using Common.Exceptions;
using Common.Models;
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

        public Staff Create(CreateStaffRequest createStaffRequest)
        {
            Staff? staffCMD = GetQueryable().FirstOrDefault(en => en.Code == createStaffRequest.Code);
            if(staffCMD != null)
            {
                throw new AppException(ExceptionCode.Notfound, "Trùng mã nhân viên");
            }

            var staff = _mapper.Map<Staff>(createStaffRequest);
            Add(staff);
            return staff;
        }
        public Staff GetDetailById(Guid id)
        {
            Staff? staff = GetById(id);
            if (staff == null){
                throw new AppException(ExceptionCode.Notfound, "Staff not found");
            }
            return GetById(id);
        }
        public Staff DeleteById(Guid staffId)
        {
            Staff? staff = GetById(staffId);
            if (staff == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Staff not found");
            }
            Account? account = _context.Accounts.FirstOrDefault(a => a.StaffId == staff.Id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            _context.Staffs.Remove(staff);
            _context.SaveChanges();

            return staff;

        }
        public PaginatedList<Staff> Filter(FilterStaffRequest request)
        {
            IQueryable<Staff> query = GetQueryable().OrderByDescending(s => s.CreatedDate).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            PaginatedList<Staff> paginatedList = new PaginatedList<Staff>(query.ToList(), CountTotal(), request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public Staff Update(UpdateStaffRequest staff)
        {
            Staff? staffCMD = GetById(staff.Id);
            if (staffCMD == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Staff not found");
            }
            var existingAccount1 = GetQueryable().FirstOrDefault(a => a.Code == staff.Code);
            if (existingAccount1 != null && existingAccount1.Id != staffCMD.Id)
                throw new AppException(ExceptionCode.Notfound, "Trùng mã nhân viên");
            _mapper.Map(staff, staffCMD);
            Update(staffCMD);
            return staffCMD;
        }
    }
}
