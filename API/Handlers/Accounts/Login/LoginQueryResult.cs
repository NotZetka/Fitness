using API.Data.Dtos;
using MediatR;

namespace API.Handlers.Accounts.Login
{
    public class LoginQueryResult 
    {
        public UserDto User { get; set; }

        public string Token { get; set; }
    }
}
