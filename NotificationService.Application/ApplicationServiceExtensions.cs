using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Consumers;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;

namespace NotificationService.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<OrderCreatedConsumer>();
            });

            services.AddScoped<INotificationApplicationService, NotificationApplicationService>();

            return services;
        }
    }
}