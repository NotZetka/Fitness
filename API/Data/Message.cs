
namespace API.Data
{
    public class Message : DbEntity
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

        public string Content { get; set; }
        public DateTime DateSend { get; set; } = DateTime.Now;

        public int SenderId { get; set; }
        public AppUser Sender { get; set; }

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
    }
}
