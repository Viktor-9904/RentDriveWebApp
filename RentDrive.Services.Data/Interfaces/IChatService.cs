
using RentDrive.Web.ViewModels.Chat;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IChatService
    {
        public Task<IEnumerable<UserChatDetails>> GetUserChatDetails(string currentUserId, string searchQuery);
        public Task<bool> SaveChatMessage(ChatMessageViewModel chatMessage);
        public Task<IEnumerable<RecentChatViewModel>> GetRecentChats(string currentUserId);
        public Task<IEnumerable<ChatMessageViewModel>> LoadChatHistory(Guid senderId, Guid receiverId);
    }
}
