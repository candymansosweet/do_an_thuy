using Common.Models;
using System;

namespace Application.Accounts.Request
{
    public class FilterAccountRequest : BasePaginatedQuery
    {
        public string? Name { get; set; }
    }
} 