using Microsoft.Extensions.DependencyInjection;
using PO.BackgroundJob.Business;
using System.Linq;

namespace PO.BackgroundJob.Repository
{
    public static class PO_ManagerServiceRegistration
    {
        public static void AddManagerServices(this IServiceCollection services)
        {
            var assembly = typeof(PO_OrdersManager).Assembly;
            var types = assembly.ExportedTypes.Where(x => x.IsClass && x.IsPublic && x.Name.EndsWith("Manager"));

            foreach (var type in types) services.AddScoped(type.GetInterface($"I{type.Name}"), type);
        }
    }
}
