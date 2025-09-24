using System.ComponentModel.DataAnnotations;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Dtos;

public record CreateNotificationRequest(
    [Required] string Recipient,
    [Required] string Message,
    [Required] NotificationType Type,
    string? Subject = null,
    Guid? ExternalReferenceId = null
);
    