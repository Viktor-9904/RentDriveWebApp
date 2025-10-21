using Microsoft.EntityFrameworkCore;

using RentDrive.Data.Models;
using RentDrive.Data.Repository.Interfaces;
using RentDrive.Services.Data.Common;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Chat;

using static RentDrive.Common.EntityValidationConstants.ApplicationUserValidationConstants.Company;

namespace RentDrive.Services.Data
{
    public class ChatService : IChatService
    {
        private readonly IRepository<ApplicationUser, Guid> applicationUserRepository;
        private readonly IRepository<ChatMessage, Guid> chatMessageRepository;
        private readonly IEncryptionService encryptionService;

        public ChatService(
            IRepository<ApplicationUser, Guid> applicationUserRepository,
            IRepository<ChatMessage, Guid> chatMessageRepository,
            IEncryptionService encryptionService)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.chatMessageRepository = chatMessageRepository;
            this.encryptionService = encryptionService;
        }

        public async Task<ServiceResponse<IEnumerable<UserChatDetails>>> GetUserChatDetails(Guid guidUserId, string searchQuery)
        {
            List<UserChatDetails> userDetails = await this.applicationUserRepository
                .GetAllAsQueryable()
                .Where(au =>
                    EF.Functions.ILike(au.UserName ?? "".ToLower(), $"%{searchQuery.ToLower()}%") &&
                    au.Id != guidUserId &&
                    au.Id != new Guid(CompanyId))
                .Select(au => new UserChatDetails()
                {
                    UserId = au.Id,
                    Username = au.UserName ?? "Unknown"
                })
                .Take(5)
                .ToListAsync();

            return ServiceResponse<IEnumerable<UserChatDetails>>.Ok(userDetails);
        }

        public async Task<ServiceResponse<bool>> SaveChatMessage(ChatMessageViewModel sentMessage)
        {
            ChatMessage newChatMessage = new ChatMessage()
            {
                SenderId = sentMessage.SenderId,
                ReceiverId = sentMessage.ReceiverId,
                Text = this.encryptionService.Encrypt(sentMessage.Text),
                TimeSent = sentMessage.TimeSent,
            };

            await this.chatMessageRepository.AddAsync(newChatMessage);
            await this.chatMessageRepository.SaveChangesAsync();

            return ServiceResponse<bool>.Ok(true);
        }

        public async Task<ServiceResponse<IEnumerable<RecentChatViewModel>>> GetRecentChats(Guid guidUserId)
        {
            List<RecentChatViewModel> recentChats = await this.chatMessageRepository
                .GetAllAsQueryable()
                .Include(cm => cm.Sender)
                .Include(cm => cm.Receiver)
                .Where(cm => cm.SenderId == guidUserId || cm.ReceiverId == guidUserId)
                .GroupBy(cm =>
                    cm.SenderId == guidUserId
                        ? cm.ReceiverId
                        : cm.SenderId
                )
                .Select(g => g
                    .OrderByDescending(cm => cm.TimeSent)
                    .Select(cm => new RecentChatViewModel
                    {
                        UserId = cm.SenderId == guidUserId ? cm.ReceiverId : cm.SenderId,
                        Username = cm.SenderId == guidUserId
                            ? (cm.Receiver.UserName ?? "Unknown")
                            : (cm.Sender.UserName ?? "Unknown")
                    })
                    .First()
                )
                .ToListAsync();

            return ServiceResponse<IEnumerable<RecentChatViewModel>>.Ok(recentChats);
        }

        public async Task<ServiceResponse<IEnumerable<ChatMessageViewModel>>> LoadChatHistory(Guid senderId, Guid receiverId)
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
                    Text = this.encryptionService.Decrypt(cm.Text),
                    TimeSent = DateTime.SpecifyKind(cm.TimeSent, DateTimeKind.Utc)
                })
                .OrderBy(cmvm => cmvm.TimeSent)
                .ToListAsync();

            return ServiceResponse<IEnumerable<ChatMessageViewModel>>.Ok(messages);
        }
    }
}
