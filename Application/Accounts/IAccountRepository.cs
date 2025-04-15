using Application.Accounts.Request;
using Common.Models;
using Domain.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Accounts
{
    public interface IAccountRepository: IGenericRepository<Account>
    {
        Account Create(CreateAccountRequest request);
        Account Update(UpdateAccountRequest request);
        Account GetDetailById(Guid id);
        PaginatedList<Account> Filter(FilterAccountRequest request);
    }
} 