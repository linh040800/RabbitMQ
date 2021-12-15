using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PO.BackgroundJob.Repository.Base;

namespace PO.BackgroundJob.Main
{
    public partial class Startup
    {
        /// <summary>
        /// Adds the Sql Server storage.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="Configuration">The configuration.</param>
        public void AddSqlServerStorage(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlServerStorageConfig>(configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<SqlServerStorage>();
        }
    }
}
