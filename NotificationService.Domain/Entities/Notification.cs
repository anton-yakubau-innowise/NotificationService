using NotificationService.Domain.Common;
using NotificationService.Domain.Enums;

namespace NotificationService.Domain.Entities;

public class Notification
{
    public Guid Id { get; private set; }
    public string Recipient { get; private set; } = null!;
    public string? Subject { get; private set; }
    public string Message { get; private set; } = null!;
    public NotificationType Type { get; private set; }
    public NotificationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public string? FailureReason { get; private set; }

    private Notification() { }

    private Notification(string recipient, string message, NotificationType type, NotificationStatus? status, string? subject)
    {
        Guard.AgainstNullOrWhiteSpace(recipient, nameof(recipient));
        Guard.AgainstNullOrWhiteSpace(message, nameof(message));

        Id = Guid.NewGuid();
        Recipient = recipient;
        Message = message;
        Type = type;
        Subject = subject;
        Status = status ?? NotificationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public static Notification CreateNotification(string recipient, string message, NotificationType type, NotificationStatus? status, string? subject)
    {
        return new Notification(recipient, message, type, status, subject);
    }


    public void MarkAsSent()
    {
        if (Status == NotificationStatus.Sent)
            throw new InvalidOperationException("Notification has already been sent.");

        Status = NotificationStatus.Sent;
        ProcessedAt = DateTime.UtcNow;
        FailureReason = null;
    }

    public void MarkAsFailed(string reason)
    {
        Guard.AgainstNullOrWhiteSpace(reason, nameof(reason));

        Status = NotificationStatus.Failed;
        ProcessedAt = DateTime.UtcNow;
        FailureReason = reason;
    }

}
