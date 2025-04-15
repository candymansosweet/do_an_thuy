using Common.Constants;

namespace Application.Authenticates.Response
{
    public class AuthenticateResponse
    { 
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AccountName { get; set; }
        public string Token { get; set; }
        public RoleValue.ROLE Role { get; set; }
    }
}