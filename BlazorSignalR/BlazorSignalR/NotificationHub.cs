using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalR;

public class NotificationHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).RecieveNotification($"کاربر {Context.User?.Identity?.Name} خوش آمدید");
    }
}
