using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hub.BackgroundJob.Repository;

namespace Hub.BackgroundJob.Main
{
    public partial class Startup
    {
        private void AddCoreDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            //DI Log
            

            ////DI Model into startup
            services.AddRepositoryServices();
            services.AddManagerServices();
        }

        /// <summary>
        /// Adds AddDependencyInjection
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="Configuration">The configuration.</param>
        public void AddDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            this.AddCoreDependencyInjection(services, configuration);
        }
    }
}
