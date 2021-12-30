using Microsoft.Extensions.DependencyInjection;
using Hub.BackgroundJob.Repository;
using System.Linq;

namespace Hub.BackgroundJob.Repository
{
    public static class PO_RepositoryServiceRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            var assembly = typeof(PO_OrdersRepository).Assembly;
            var types = assembly.ExportedTypes.Where(x => x.IsClass && x.IsPublic && x.Name.EndsWith("Repository"));
            foreach (var type in types) services.AddScoped(type.GetInterface($"I{type.Name}"), type);
        }
    }
}
