using MassTransit;
using Contracts;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Dtos;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Common;
using Microsoft.Extensions.Logging;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(INotificationApplicationService notificationApplicationService, ILogger<OrderCreatedConsumer> logger) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        await SendEmailNotification(context, message);

        await SendSmsNotification(context, message);
    }

    private async Task SendEmailNotification(ConsumeContext<OrderCreatedEvent> context, OrderCreatedEvent message)
    {
        if (!string.IsNullOrWhiteSpace(message.CustomerEmail))
        {
            Guard.AgainstInvalidEmail(message.CustomerEmail);

            var emailMessage = FormatOrderPlacedMessage(message);
            var emailRequest = new CreateNotificationRequest(
                Recipient: message.CustomerEmail,
                Message: emailMessage,
                Type: NotificationType.Email,
                Subject: $"Order Confirmation {message.OrderId}",
                ExternalReferenceId: message.OrderId
            );

            await notificationApplicationService.CreateNotificationAsync(emailRequest, context.CancellationToken);
        }
        else
        {
            logger.LogWarning("OrderCreatedEvent received without a valid CustomerEmail. OrderId: {OrderId}", message.OrderId);
        }
    }

    private async Task SendSmsNotification(ConsumeContext<OrderCreatedEvent> context, OrderCreatedEvent message)
    {
        if (!string.IsNullOrWhiteSpace(message.CustomerPhoneNumber))
        {
            Guard.AgainstInvalidPhoneNumber(message.CustomerPhoneNumber);

            var smsMessage = FormatOrderPlacedMessage(message);
            var smsRequest = new CreateNotificationRequest(
                Recipient: message.CustomerPhoneNumber,
                Message: smsMessage,
                Type: NotificationType.Sms,
                Subject: null,
                ExternalReferenceId: message.OrderId
            );

            await notificationApplicationService.CreateNotificationAsync(smsRequest, context.CancellationToken);
        }
        else
        {
            logger.LogWarning("OrderCreatedEvent received without a valid CustomerPhoneNumber. OrderId: {OrderId}", message.OrderId);
        }
    }

    private string FormatOrderPlacedMessage(OrderCreatedEvent message)
    {
        return $"Your order {message.OrderId} for the amount of {message.TotalPrice.Amount} {message.TotalPrice.CurrencyCode} has been successfully placed.";
    }
}
