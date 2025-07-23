using NotificationService.Domain.Enums;

namespace NotificationService.Application.Dtos;

public record NotificationDto(
    Guid Id,
    string Recipient,
    string? Subject,
    NotificationType Type,
    NotificationStatus Status,
    DateTime CreatedAt,
    DateTime? ProcessedAt,
    DateTime? UpdatedAt,
    string? FailureReason
);
