using MediatR;

namespace API.Handlers.Accounts.Login
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
