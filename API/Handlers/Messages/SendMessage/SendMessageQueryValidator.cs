using FluentValidation;

namespace API.Handlers.Messages.SendMessage
{
    public class SendMessageQueryValidator : AbstractValidator<SendMessageQuery>
    {
        public SendMessageQueryValidator() 
        {
            RuleFor(x=>x.Content).NotEmpty().NotNull();
            RuleFor(x=>x.ReceiverId).NotEmpty().NotNull();
        }
    }
}
