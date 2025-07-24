using NotificationService.Application.Dtos;

namespace NotificationService.Application.Interfaces;

public interface INotificationApplicationService
{
    Task<NotificationDto?> GetNotificationByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<NotificationWithMessageDto?> GetNotificationWithMessageByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(CancellationToken cancellationToken = default);
    Task<Guid> CreateNotificationAsync(CreateNotificationRequest request, CancellationToken cancellationToken = default);
    Task<Guid> CreateDefaultNotificationAsync(CreateDefaultNotificationRequest request, CancellationToken cancellationToken = default);
    Task UpdateNotificationContentAsync(Guid id, UpdateNotificationContentRequest request, CancellationToken cancellationToken = default);
    Task DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default);
    Task RetryNotificationAsync(Guid id, CancellationToken cancellationToken = default);
    Task RetryNotificationsAsync(RetryNotificationsRequest request, CancellationToken cancellationToken = default);
}