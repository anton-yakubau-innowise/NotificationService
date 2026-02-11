using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Background
{
    public class NotificationSendingWorker(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationApplicationService>();
                    var pendingNotifications = await notificationService.GetAllNotificationsAsync(stoppingToken);
                    foreach (var notification in pendingNotifications)
                    {
                        if (notification.Status == NotificationStatus.Pending)
                        {
                            await notificationService.SendNotificationAsync(notification.Id, stoppingToken);
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
