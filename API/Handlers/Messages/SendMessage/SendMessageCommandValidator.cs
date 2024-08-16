using FluentValidation;

namespace API.Handlers.Messages.SendMessage
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator() 
        {
            RuleFor(x=>x.Content).NotEmpty().NotNull();
            RuleFor(x=>x.ReceiverId).NotEmpty().NotNull();
        }
    }
}
