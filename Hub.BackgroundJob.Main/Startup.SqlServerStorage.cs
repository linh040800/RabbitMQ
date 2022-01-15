using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hub.BackgroundJob.Repository.Base;

namespace Hub.BackgroundJob.Main
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
