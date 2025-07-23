using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace NotificationService.API
{
    public static class ApiServiceExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}