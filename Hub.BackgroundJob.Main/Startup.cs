using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hub.BackgroundJob.Main.Middlewares;
using Hub.BackgroundJob.Repository.Base;
using Hub.EventBus.Main.Models;
using Hub.EventBus.Main.IntegrationEvents.EventHandling;

namespace Hub.BackgroundJob.Main
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Config API version
            services.AddApiVersioning();

            services.Configure<ConfigAudience>(Configuration.GetSection("Audience"));

            this.AddSqlServerStorage(services, this.Configuration);

            //Add DependencyInjection class
            this.AddDependencyInjection(services, this.Configuration);

            //Add startup partial class configure services 
            this.ConfigureSwagger(services);

            //Config Automap
            var autoFacConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            autoFacConfig.AssertConfigurationIsValid();
            services.AddSingleton(autoFacConfig.CreateMapper());
            services.AddSingleton<IConfiguration>(Configuration);


            //Register EvenBus
            services.AddCustomHealthCheck(Configuration);
            this.RegisterEventBus(services);

            services.AddTransient<OrdersIntegrationEventHandler>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region Use startup partial class configure services
            UseSwaggerUI(app, env);
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase)) app.UsePathBase(pathBase);
            
            this.ConfigureEventBus(app);
        }
    }
}
