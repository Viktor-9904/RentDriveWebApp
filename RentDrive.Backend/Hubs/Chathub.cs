using Microsoft.AspNetCore.SignalR;
using RentDrive.Services.Data.Interfaces;
using RentDrive.Web.ViewModels.Chat;
using System.Security.Claims;

public class ChatHub : Hub
{
    private readonly IChatService chatService;
    private readonly IAccountService accountService;

    public ChatHub(
        IChatService chatService,
        IAccountService accountService)
    {
        this.chatService = chatService;
        this.accountService = accountService;
    }

    public async Task SendMessage(SentChatMessgeViewModel sentMessage)
    {
        string? currentUserId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId == null)
        {
            return;
        }

        if (!await accountService.Exists(currentUserId))
        {
            return;
        }

        Guid senderId = Guid.Parse(currentUserId);
        DateTime timeSent = DateTime.UtcNow;

        ChatMessageViewModel chatMessage = new ChatMessageViewModel()
        {
            ReceiverId = sentMessage.ReceiverId,
            SenderId = senderId,
            TimeSent = timeSent,
            Text = sentMessage.Text,
        };

        bool wasMessageSaved = await chatService.SaveChatMessage(chatMessage);

        if (wasMessageSaved)
        {
            await Clients.User(sentMessage.ReceiverId.ToString())
                .SendAsync("ReceiveMessage", sentMessage);
        }
    }
}