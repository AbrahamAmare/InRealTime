using InRealTime.Data;
using InRealTime.Extensions;
using InRealTime.Models.ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace InRealTime.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatRegistry _registry;
        private readonly AppDbContext _dbContext;
        public ChatHub(ChatRegistry registry, AppDbContext dbContext)
        {
            _registry = registry;
            _dbContext = dbContext;
        }

        public async Task SendInbox(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveInbox", user, message, DateTimeOffset.UtcNow);
        }

        public async Task<List<OutputMessage>> JoinRoom(RoomRequest request)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, request.Room);

            return _registry.GetRoomMessages(request.Room)
                .Select(m => m.Output)
                .ToList();
        }

        public Task LeaveRoom(RoomRequest request)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, request.Room);
        }

        public async Task SendMessage(InputMessage message)
        {
            var userId = Context.User?.GetUserId();

            var user = await _dbContext.Users.FindAsync(Guid.Parse(userId));

            var userMessage = new UserMessage(user, message.Message, message.Room);

            _registry.AddMessage(message.Room, userMessage);

            await Clients.GroupExcept(message.Room, new[] { Context.ConnectionId })
                .SendAsync("sendMessage", userMessage);
        }
    }
}
