
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalR;

public class ServerTimeNotifier : BackgroundService
{
    private static readonly TimeSpan period = TimeSpan.FromSeconds(5);
    private readonly ILogger<ServerTimeNotifier> _logger;
    private readonly IHubContext<NotificationHub, INotificationClient> _context;

    public ServerTimeNotifier(
        ILogger<ServerTimeNotifier> logger,
        IHubContext<NotificationHub,INotificationClient> context)
    {
        _logger = logger;
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(period);

        while(!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            var dateTime = DateTime.Now;
            _logger.LogInformation("Executing (service)(time)", nameof(ServerTimeNotifier), dateTime);

            _context.Clients.All.RecieveNotification($"Server time={dateTime}");
        }
    }
}
