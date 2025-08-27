using NotificationService.Domain.Enums;

namespace NotificationService.Application.Dtos;
public record NotificationWithMessageDto(
    Guid Id,
    string Recipient,
    string? Subject,
    NotificationType Type,
    NotificationStatus Status,
    string Message,
    DateTime CreatedAt,
    DateTime? ProcessedAt,
    DateTime? UpdatedAt,
    string? FailureReason
);