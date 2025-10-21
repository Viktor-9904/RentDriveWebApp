using Microsoft.AspNetCore.Mvc;
using RentDrive.Services.Data.Common;
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
                return Unauthorized("Unauthorized User!");
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(currentUserId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<IEnumerable<UserChatDetails>> response = await this.chatMessageService
                .GetUserChatDetails(guidUserId, searchQuery);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("recent-chats")]
        public async Task<IActionResult> GetUserRecentChats()
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized("Unauthorized User!");
            }

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(currentUserId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<IEnumerable<RecentChatViewModel>> response = await this.chatMessageService
                .GetRecentChats(guidUserId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }

        [HttpGet("load-chat-history/{receiverId}")]
        public async Task<IActionResult> LoadChatHistory(Guid receiverId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid guidUserId = Guid.Empty;
            if (!IsGuidValid(currentUserId, ref guidUserId))
            {
                return Unauthorized("Unauthorized User!");
            }

            ServiceResponse<IEnumerable<ChatMessageViewModel>> response = await this.chatMessageService
                .LoadChatHistory(guidUserId, receiverId);

            if (!response.Success)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Result);
        }
    }
}
