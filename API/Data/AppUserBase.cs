using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public abstract class AppUserBase : IdentityUser<int>
    {
        public string? PhotoUrl { get; set; }
        public string Role { get; set; }

        public IList<Message> MessagesSend { get; set; }
        public IList<Message> MessagesReceived { get; set; }
    }
}
