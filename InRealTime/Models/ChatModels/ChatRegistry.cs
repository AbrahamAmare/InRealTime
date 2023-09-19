namespace InRealTime.Models.ChatModels
{
    public class ChatRegistry
    {
        private readonly Dictionary<string, List<UserMessage>> _roomMessages = new();

        public void CreateRoom(string room)
        {
            _roomMessages[room] = new();
        }

        public void AddMessage(string room, UserMessage message)
        {
            _roomMessages[room].Add(message);
        }

        public List<UserMessage> GetRoomMessages(string room)
        {
            return _roomMessages[room];
        }

        public List<string> GetRooms()
        {
            return _roomMessages.Keys.ToList();
        }
    }
}
