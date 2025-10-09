using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Chat;
using System.Security.Claims;

namespace RentDrive.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseController
    {
        private readonly IChatService chatMessageService;

        public ChatController(
            IChatService chatMessageService,
            IBaseService baseService) : base(baseService)
        {
            this.chatMessageService = chatMessageService;
        }

        [HttpGet("search-users/{searchQuery}")]
        public async Task<IActionResult> GetUsersBySearchQuery(string searchQuery)
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            IEnumerable<UserChatDetails> availableUsers = await this.chatMessageService
                .GetUserChatDetails(currentUserId, searchQuery);

            return Ok(availableUsers);
        }
        [HttpGet("recent-chats")]
        public async Task<IActionResult> GetUserRecentChats()
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            IEnumerable<RecentChatViewModel> recentChats = await this.chatMessageService
                .GetRecentChats(currentUserId);

            return Ok(recentChats);
        }
        [HttpGet("load-chat-history/{receiverId}")]
        public async Task<IActionResult> LoadChatHistory(Guid receiverId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid guidCurrentUserId = Guid.Empty;
            bool isCurrentUserValid = IsGuidValid(currentUserId!, ref guidCurrentUserId);

            if (!isCurrentUserValid || receiverId == Guid.Empty)
            {
                return Unauthorized();
            }

            IEnumerable<ChatMessageViewModel> messages = await this.chatMessageService
                .LoadChatHistory(guidCurrentUserId, receiverId);

            return Ok(messages);
        }
    }
}
