
using RentDrive.Services.Data.Common;
using RentDrive.Web.ViewModels.Chat;

namespace RentDrive.Services.Data.Interfaces
{
    public interface IChatService
    {
        public Task<ServiceResponse<IEnumerable<UserChatDetails>>> GetUserChatDetails(Guid guidUserId, string searchQuery);
        public Task<ServiceResponse<bool>> SaveChatMessage(ChatMessageViewModel chatMessage);
        public Task<ServiceResponse<IEnumerable<RecentChatViewModel>>> GetRecentChats(Guid guidUserId);
        public Task<ServiceResponse<IEnumerable<ChatMessageViewModel>>> LoadChatHistory(Guid senderId, Guid receiverId);
    }
}
