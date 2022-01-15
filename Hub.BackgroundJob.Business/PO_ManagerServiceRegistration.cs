using Microsoft.Extensions.DependencyInjection;
using Hub.BackgroundJob.Business;
using System.Linq;
using Hub.EventBus;

namespace Hub.BackgroundJob.Repository
{
    public static class PO_ManagerServiceRegistration
    {
        public static void AddManagerServices(this IServiceCollection services)
        {
            var assembly = typeof(PO_OrdersManager).Assembly;
            var types = assembly.ExportedTypes.Where(x => x.IsClass && x.IsPublic && x.Name.EndsWith("Manager"));

            foreach (var type in types) services.AddScoped(type.GetInterface($"I{type.Name}"), type);

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }
    }
}
