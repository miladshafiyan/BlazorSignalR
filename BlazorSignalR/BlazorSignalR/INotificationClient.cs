namespace BlazorSignalR;

public interface INotificationClient
{
    Task RecieveNotification(string message);
}
