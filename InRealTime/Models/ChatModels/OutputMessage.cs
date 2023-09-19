namespace InRealTime.Models.ChatModels
{
    public class OutputMessage
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Room { get; set; }
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;

        public OutputMessage(
                string message,
                string username,
                string room
            )
        {
            Message = message; UserName = username; Room = room;
        }
    }
}
