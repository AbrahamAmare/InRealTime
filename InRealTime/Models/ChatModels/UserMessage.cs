using InRealTime.Data.Entities;

namespace InRealTime.Models.ChatModels
{
    public class UserMessage
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string Room { get; set; }

        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;

        public OutputMessage Output
        {
            get
            {
                return new(Message, User.UserName, Room);
            }
        }

        // Navigation
        public User User { get; set; }

        public UserMessage(
                User user,
                string message,
                string room
            )
        {
            User = user;
            Message = message;
            Room = room;
        }
    }
}
