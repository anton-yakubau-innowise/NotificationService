using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Dtos;
using NotificationService.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(INotificationApplicationService notificationService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NotificationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllNotifications(CancellationToken cancellationToken)
    {
        var notifications = await notificationService.GetAllNotificationsAsync(cancellationToken);
        return Ok(notifications);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificationById(Guid id, CancellationToken cancellationToken)
    {
        var notification = await notificationService.GetNotificationByIdAsync(id, cancellationToken);
        return Ok(notification);
    }

    [HttpGet("{id:guid}/with-message")]
    [ProducesResponseType(typeof(NotificationWithMessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotificationByIdWithMessage(Guid id, CancellationToken cancellationToken)
    {
        var notification = await notificationService.GetNotificationWithMessageByIdAsync(id, cancellationToken);
        return Ok(notification);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequest request, CancellationToken cancellationToken)
    {
        var notificationId = await notificationService.CreateNotificationAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetNotificationById), new { id = notificationId }, notificationId);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateNotificationContent(Guid id, [FromBody] UpdateNotificationContentRequest request, CancellationToken cancellationToken)
    {
        await notificationService.UpdateNotificationContentAsync(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNotification(Guid id, CancellationToken cancellationToken)
    {
        await notificationService.DeleteNotificationAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/retry")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RetryNotification(Guid id, CancellationToken cancellationToken)
    {
        await notificationService.RetryNotificationAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpPost("retry")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RetryNotifications([FromBody] RetryNotificationsRequest request, CancellationToken cancellationToken)
    {
        await notificationService.RetryNotificationsAsync(request, cancellationToken);
        return NoContent();
    }
}