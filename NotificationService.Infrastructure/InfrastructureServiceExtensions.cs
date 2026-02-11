using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotificationService.Application.Consumers;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Repositories;
using NotificationService.Infrastructure.Options;
using NotificationService.Infrastructure.Persistence;
using NotificationService.Infrastructure.Persistence.Repositories;

namespace NotificationService.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.SectionName));

        services.AddOptions<AzureServiceBusOptions>()
            .Bind(configuration.GetSection(AzureServiceBusOptions.SectionName));
            
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<OrderCreatedConsumer>();
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            var asbOptions = configuration.GetSection(AzureServiceBusOptions.SectionName).Get<AzureServiceBusOptions>();
            var connectionString = asbOptions?.ConnectionString;

            if (!string.IsNullOrEmpty(connectionString))
            {
                busConfigurator.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(connectionString);
                    
                    cfg.ConfigureEndpoints(context);
                });
            }
            else
            {
                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                    cfg.Host(options.Host, options.VirtualHost, h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });

                    cfg.ReceiveEndpoint(options.OrderCreatedQueueName, e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(context);
                    });
                });
            }
        });

        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}