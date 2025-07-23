using System.ComponentModel.DataAnnotations;

namespace NotificationService.Application.Dtos;

public record RetryNotificationsRequest(
    [Required]
    [MinLength(1)]
    IEnumerable<Guid> NotificationIds
);
