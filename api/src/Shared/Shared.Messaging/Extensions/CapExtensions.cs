using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging.Extensions
{
    public static class CapExtensions
    {
        public static IServiceCollection AddCapWithRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCap(x =>
            {
                x.UsePostgreSql(configuration.GetConnectionString("Database")!);

                x.UseRabbitMQ(rabbit =>
                {
                    rabbit.HostName = configuration["MessageBroker:Host"] ?? "localhost";
                    rabbit.UserName = configuration["MessageBroker:UserName"] ?? "guest";
                    rabbit.Password = configuration["MessageBroker:Password"] ?? "guest";
                });

                x.UseDashboard(); // CAP dashboard localhost:5173/cap

                x.FailedRetryCount = 5;
                x.FailedThresholdCallback = failed =>
                {
                    // Burada loglama veya uyarı mekanizması
                };
            });

            return services;
        }
    }
}