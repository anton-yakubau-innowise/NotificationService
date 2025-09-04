using MassTransit;
using Contracts;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Dtos;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(INotificationApplicationService notificationApplicationService) : IConsumer<OrderCreatedEvent>
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
            var emailMessage = $"Your order {message.OrderId} for the amount of {message.TotalPrice.Amount} {message.TotalPrice.CurrencyCode} has been successfully placed.";
            var emailRequest = new CreateNotificationRequest(
                Recipient: message.CustomerEmail,
                Message: emailMessage,
                Type: NotificationType.Email,
                Subject: $"Order Confirmation {message.OrderId}"
            );

            await notificationApplicationService.CreateNotificationAsync(emailRequest, context.CancellationToken);
        }
    }

    private async Task SendSmsNotification(ConsumeContext<OrderCreatedEvent> context, OrderCreatedEvent message)
    {
        if (!string.IsNullOrWhiteSpace(message.CustomerPhoneNumber))
        {
            var smsMessage = $"Your order {message.OrderId} for the amount of {message.TotalPrice.Amount} {message.TotalPrice.CurrencyCode} has been successfully placed.";
            var smsRequest = new CreateNotificationRequest(
                Recipient: message.CustomerPhoneNumber,
                Message: smsMessage,
                Type: NotificationType.Sms,
                Subject: null
            );

            await notificationApplicationService.CreateNotificationAsync(smsRequest, context.CancellationToken);
        }
    }
}