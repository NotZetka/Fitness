namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<int, List<string>> OnlineUsers = new Dictionary<int, List<string>>();

        public Task UserConnected(int userId, string connectionId)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(userId))
                {
                    OnlineUsers[userId].Add(connectionId);
                }
                else
                {
                    OnlineUsers.Add(userId, new List<string>());
                }
            }

            return Task.CompletedTask;
        }

        public Task UserDisconected(int userId, string connectionId) 
        {
            lock (OnlineUsers) 
            {
                if(!OnlineUsers.ContainsKey(userId)) return Task.CompletedTask;

                OnlineUsers[userId].Remove(connectionId);

                if (OnlineUsers[userId].Count == 0) 
                {
                    OnlineUsers.Remove(userId);
                }
            }

            return Task.CompletedTask;
        }

        public Task<int[]> GetOnlineUsers() 
        {
            int[] onlineUsersIds;
            lock (OnlineUsers)
            { 
                onlineUsersIds = OnlineUsers.OrderBy(k=>k.Key).Select(k=>k.Key).ToArray();
            }

            return Task.FromResult(onlineUsersIds);
        }
    }
}
