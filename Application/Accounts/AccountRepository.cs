using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Accounts.Request;
using Common.Exceptions;
using Common.Services.JwtTokenService;
using Common.Models;

namespace Application.Accounts
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AccountRepository(IJwtTokenService jwtTokenService, 
        ApplicationContext context, IMapper mapper): base(context, mapper)
        {
            _jwtTokenService = jwtTokenService;
        }

        public Account Create(CreateAccountRequest request)
        {
            var existingAccount = GetQueryable().FirstOrDefault(a => a.Name == request.Name);
            if (existingAccount != null)
                throw new AppException("Account already exists");

            var account = new Account
            {
                Name = request.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                StaffId = request.StaffId,
                Role = request.Role
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Account Update(UpdateAccountRequest request)
        {

            var existingAccount = GetQueryable().FirstOrDefault(a => a.Id == request.Id);
            if (existingAccount == null)
                throw new AppException(ExceptionCode.Notfound, "Account not found");

            var existingAccount1 = GetQueryable().FirstOrDefault(a => a.Name == request.Name);
            if (existingAccount1 != null && existingAccount1 != existingAccount)
                throw new AppException(ExceptionCode.Notfound, "Trùng tên đăng nhập với tài khoản khác");

            existingAccount.Name = request.Name;
            existingAccount.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            existingAccount.StaffId = request.StaffId;
            existingAccount.Role = request.Role;

            _context.Accounts.Update(existingAccount);
            _context.SaveChanges();
            return existingAccount;
        }

        public Account GetDetailById(Guid id)
        {
            var account = GetQueryable()
                .FirstOrDefault(a => a.Id == id);

            if (account == null)
                throw new AppException(ExceptionCode.Notfound, "Account not found");

            return account;
        }

        public Account DeleteById(Guid id)
        {
            var account = GetById(id);
            if (account == null)
                throw new AppException(ExceptionCode.Notfound, "Account not found");

            Delete(account);
            return account;
        }

        public PaginatedList<Account> Filter(FilterAccountRequest request)
        {
            IQueryable<Account> query = GetQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(a => a.Name.Contains(request.Name));
            }

            //if (request.StaffId.HasValue)
            //{
            //    query = query.Where(a => a.StaffId == request.StaffId.Value);
            //}

            query = query.OrderByDescending(a => a.CreatedDate)
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize);

            var accounts = query.ToList();
            return new PaginatedList<Account>(accounts, CountTotal(), request.PageNumber, request.PageSize);
        }
    }
} 