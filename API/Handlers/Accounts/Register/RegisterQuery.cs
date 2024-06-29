using MediatR;

namespace API.Handlers.Accounts.Register
{
    public class RegisterQuery : IRequest<RegisterQueryResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
