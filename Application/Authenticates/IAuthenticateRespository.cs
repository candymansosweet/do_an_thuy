using Application.Accounts.Request;
using Application.Authenticates.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authenticates
{
    public interface IAuthenticateRespository
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest request, string secretString);
        // public bool Logout(Guid userId, string secretString);
    }
}
