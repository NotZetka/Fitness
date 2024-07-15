namespace API.Data.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

        public string Content { get; set; }
        public DateTime DateSend { get; set; } = DateTime.Now;

        public int ReceiverId { get; set; }
    }
}
