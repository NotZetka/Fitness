using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Handlers.Accounts.Register.Trainer
{
    public class RegisterTrainerCommand : IRequest<RegisterResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
