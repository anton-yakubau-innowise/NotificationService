using System.ComponentModel.DataAnnotations;

namespace NotificationService.Infrastructure.Options;

public class RabbitMqOptions
{
    public const string SectionName = "MassTransit:RabbitMq"; 

    public string Host { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string OrderCreatedQueueName { get; set; } = string.Empty;
}