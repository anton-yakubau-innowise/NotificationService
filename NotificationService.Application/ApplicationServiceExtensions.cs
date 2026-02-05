using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Background;
using NotificationService.Application.Consumers;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;

namespace NotificationService.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationApplicationService, NotificationApplicationService>();

            services.AddHostedService<NotificationSendingWorker>();
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<OrderCreatedConsumer>();
            });


            return services;
        }
    }
}