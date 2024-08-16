namespace API.Handlers.Messages.SendMessage
{
    public class SendMessageCommand
    {
        public int ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
