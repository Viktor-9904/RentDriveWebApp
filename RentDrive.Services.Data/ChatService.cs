using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Chat;

using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.Company;

namespace RentDrive.Services.Data
{
    public class ChatService : IChatService
    {
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;
        private readonly IRepository<ChatMessage, Guid> chatMessageRepository;

        public ChatService(
            IRepository<ApplicationUser, Guid> applicationUserRepository,
            IRepository<ChatMessage, Guid> chatMessageRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.chatMessageRepository = chatMessageRepository;
        }

        public async Task<IEnumerable<UserChatDetails>> GetUserChatDetails(string currentUserId, string searchQuery)
        {
            if (!Guid.TryParse(currentUserId, out var currentUserGuid))
                return [];

            List<UserChatDetails> userDetails = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Where(au =>
                    EF.Functions.ILike(au.UserName ?? "".ToLower(), $"%{searchQuery.ToLower()}%") &&
                    au.Id != currentUserGuid &&
                    au.Id != new Guid(CompanyId))
                .Select(au => new UserChatDetails()
                {
                    UserId = au.Id,
                    Username = au.UserName ?? "Unknown"
                })
                .Take(5)
                .ToListAsync();

            return userDetails;
        }

        public async Task<bool> SaveChatMessage(ChatMessageViewModel sentMessage)
        {
            ChatMessage newChatMessage = new ChatMessage()
            {
                SenderId = sentMessage.SenderId,
                ReceiverId = sentMessage.ReceiverId,
                Text = sentMessage.Text,
                TimeSent = sentMessage.TimeSent,
            };

            await this.chatMessageRepository.AddAsync(newChatMessage);
            await this.chatMessageRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RecentChatViewModel>> GetRecentChats(string currentUserId)
        {
            if (!Guid.TryParse(currentUserId, out var currentUserGuid))
                return [];

            List<RecentChatViewModel> recentChats = await this.chatMessageRepository
                .GetAllAsQueryable()
                .Include(cm => cm.Sender)
                .Include(cm => cm.Receiver)
                .Where(cm => cm.SenderId == currentUserGuid || cm.ReceiverId == currentUserGuid)
                .GroupBy(cm =>
                    cm.SenderId == currentUserGuid
                        ? cm.ReceiverId
                        : cm.SenderId
                )
                .Select(g => g
                    .OrderByDescending(cm => cm.TimeSent)
                    .Select(cm => new RecentChatViewModel
                    {
                        UserId = cm.SenderId == currentUserGuid ? cm.ReceiverId : cm.SenderId,
                        Username = cm.SenderId == currentUserGuid
                            ? (cm.Receiver.UserName ?? "Unknown")
                            : (cm.Sender.UserName ?? "Unknown")
                    })
                    .First()
                )
                .ToListAsync();

            return recentChats;
        }

        public async Task<IEnumerable<ChatMessageViewModel>> LoadChatHistory(Guid senderId, Guid receiverId)
        {
            IEnumerable<ChatMessageViewModel> messages = await this.chatMessageRepository
                .GetAllAsQueryable()
                .Where(cm =>
                    (cm.ReceiverId == receiverId && cm.SenderId == senderId) ||
                    (cm.ReceiverId == senderId && cm.SenderId == receiverId))
                .Select(cm => new ChatMessageViewModel()
                {
                    SenderId = cm.SenderId,
                    ReceiverId = cm.ReceiverId,
                    Text = cm.Text,
                    TimeSent = cm.TimeSent
                })
                .OrderBy(cmvm => cmvm.TimeSent)
                .ToListAsync();

            return messages;
        }
    }
}
