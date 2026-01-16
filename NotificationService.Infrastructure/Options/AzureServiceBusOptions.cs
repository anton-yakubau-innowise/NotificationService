namespace NotificationService.Infrastructure.Options;

public class AzureServiceBusOptions
{
    public const string SectionName = "MassTransit:AzureServiceBus"; 

    public string ConnectionString { get; set; } = string.Empty;
}