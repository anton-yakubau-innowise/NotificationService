using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Background
{
    public class NotificationSendingWorker(INotificationApplicationService notificationService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var pendingNotifications = await notificationService.GetAllNotificationsAsync(stoppingToken);
                foreach (var notification in pendingNotifications)
                {
                    if (notification.Status == NotificationStatus.Pending)
                    {
                        await notificationService.SendNotificationAsync(notification.Id, stoppingToken);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
