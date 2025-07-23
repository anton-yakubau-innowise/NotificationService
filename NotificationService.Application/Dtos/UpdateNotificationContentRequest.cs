namespace NotificationService.Application.Dtos;

public record UpdateNotificationContentRequest(
    string? Recipient = null,
    string? Message = null,
    string? Subject = null
);