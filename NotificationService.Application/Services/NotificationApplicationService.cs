using AutoMapper;
using NotificationService.Application.Dtos;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Services;

public class NotificationApplicationService(IUnitOfWork unitOfWork, IMapper mapper) : INotificationApplicationService
{

    public async Task<NotificationDto?> GetNotificationByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await GetNoTrackingNotificationAndEnsureExistsAsync(id, cancellationToken);

        return mapper.Map<NotificationDto>(notification);
    }

    public async Task<NotificationWithMessageDto?> GetNotificationWithMessageByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await GetNoTrackingNotificationAndEnsureExistsAsync(id, cancellationToken);

        return mapper.Map<NotificationWithMessageDto>(notification);
    }

    public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(CancellationToken cancellationToken)
    {
        var notifications = await unitOfWork.Notifications.ListAllAsNoTrackingAsync(cancellationToken);

        return mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<Guid> CreateNotificationAsync(CreateNotificationRequest request, CancellationToken cancellationToken)
    {

        var existing = await FindExistingNotificationAsync(request.ExternalReferenceId, request.Recipient, request.Type, cancellationToken);
        if (existing != null)
        {
            return existing.Id;
        }

        var notification = Notification.CreateNotification(
            request.Recipient,
            request.Message,
            request.Type,
            NotificationStatus.Pending,
            request.Subject,
            request.ExternalReferenceId
        );

        await unitOfWork.Notifications.AddAsync(notification, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return notification.Id;
    }

    public async Task UpdateNotificationContentAsync(Guid id, UpdateNotificationContentRequest request, CancellationToken cancellationToken)
    {
        var notification = await GetNotificationAndEnsureExistsAsync(id, cancellationToken);

        notification.UpdateContent(
            request.Recipient,
            request.Message,
            request.Subject
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNotificationAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await GetNotificationAndEnsureExistsAsync(id, cancellationToken);

        unitOfWork.Notifications.Delete(notification);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RetryNotificationAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await GetNotificationAndEnsureExistsAsync(id, cancellationToken);

        notification.Retry();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RetryNotificationsAsync(RetryNotificationsRequest request, CancellationToken cancellationToken)
    {
        var notifications = await unitOfWork.Notifications.ListAsync(
            n => request.NotificationIds.Contains(n.Id),
            cancellationToken
        );

        foreach (var notification in notifications)
        {
            notification.Retry();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task SendNotificationAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await GetNotificationAndEnsureExistsAsync(id, cancellationToken);

        notification.MarkAsSent();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<Notification> GetNotificationAndEnsureExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await unitOfWork.Notifications.GetByIdAsync(id, cancellationToken);

        if (notification is null)
        {
            throw new KeyNotFoundException($"Notification with ID {id} not found.");
        }

        return notification;
    }

    private async Task<Notification> GetNoTrackingNotificationAndEnsureExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await unitOfWork.Notifications.GetByIdAsNoTrackingAsync(id, cancellationToken);

        if (notification is null)
        {
            throw new KeyNotFoundException($"Notification with ID {id} not found.");
        }

        return notification;
    }
    
    private async Task<Notification?> FindExistingNotificationAsync(Guid? externalReferenceId, string recipient, NotificationType type, CancellationToken cancellationToken)
    {
        return (await unitOfWork.Notifications.ListAsync(
            n => n.ExternalReferenceId == externalReferenceId
                && n.Recipient == recipient
                && n.Type == type,
            cancellationToken)).FirstOrDefault();
    }
}
