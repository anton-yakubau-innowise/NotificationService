using System.ComponentModel.DataAnnotations;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Dtos;

public record CreateDefaultNotificationRequest(
    [Required] string Recipient,
    [Required] string Message,
    string? Subject
);
    