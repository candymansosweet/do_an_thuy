using Application.Accounts;
using Application.Accounts.Request;
using Application.Authenticates.Response;
using Application.Staffs;
using Azure;
using Common.Exceptions;
using Common.Services.JwtTokenService;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authenticates
{
    public class AuthenticateRespository: IAuthenticateRespository
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStaffRespository _staffRespository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticateRespository(
            IAccountRepository accountRepository,
            IStaffRespository staffRespository,
            IJwtTokenService jwtTokenService
            )
        {
            _accountRepository = accountRepository;
            _staffRespository = staffRespository;
            _jwtTokenService = jwtTokenService;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest request, string secretString)
        {
            Account? user = _accountRepository.GetQueryable().FirstOrDefault(a => a.Name == request.UserName);
            if (user == null)
            {
                throw new AppException("Không tìm thấy Account " + request.UserName);
            }

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new AppException(ExceptionCode.Invalidate, "Username or password is incorrect");
            Staff staff = _staffRespository.GetQueryable().FirstOrDefault(s => s.Id == user.StaffId);
            if (staff == null)
                throw new AppException("Không tìm thấy Staff " + user.StaffId);
            AuthenticateResponse rs = new AuthenticateResponse();
            rs.UserId = user.Id;
            rs.UserName = user.Name;
            rs.AccountName = staff.FullName;
            rs.Role = user.Role;
            rs.Token = _jwtTokenService.GenerateToken<Guid>(rs.UserId, secretString);
            return rs;
        }
    }
}
