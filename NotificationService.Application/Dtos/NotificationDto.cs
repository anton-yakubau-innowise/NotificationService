using NotificationService.Domain.Enums;

namespace NotificationService.Application.Dtos;

public record NotificationDto(
    Guid Id,
    string Recipient,
    Guid? ExternalReferenceId,
    string? Subject,
    NotificationType Type,
    NotificationStatus Status,
    DateTime CreatedAt,
    DateTime? ProcessedAt,
    DateTime? UpdatedAt,
    string? FailureReason
);
