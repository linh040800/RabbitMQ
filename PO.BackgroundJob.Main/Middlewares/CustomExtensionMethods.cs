using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PO.BackgroundJob.Main.Middlewares
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder.AddRabbitMQ($"amqp://{configuration["EventBusConnection"]}", name: "basket-rabbitmqbus-check", tags: new string[] { "rabbitmqbus" });
            return services;
        }
    }
}
