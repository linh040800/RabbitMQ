using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PO.BackgroundJob.Main.IntegrationEvents.Events;
using PO.EventBus;
using PO.EventBus.Abstractions;
using PO.EventBus.Main.IntegrationEvents.EventHandling;
using PO.EventBus.Main.IntegrationEvents.Events;
using PO.EventBusRabbitMQ;
using PO.EventBusRabbitMQ.Abstractions;
using RabbitMQ.Client;

namespace PO.BackgroundJob.Main
{
    public partial  class Startup 
    {/// <summary>
     /// Register the Swagger generator, defining one or more Swagger documents
     /// </summary>
     /// <param name="services">The services.</param>
        public void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"])) factory.UserName = Configuration["EventBusUserName"];
                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"])) factory.Password = Configuration["EventBusPassword"];
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"])) retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var retryCount = 5;
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"])) retryCount = int.Parse(Configuration["EventBusRetryCount"]);

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<ProductPriceChangedIntegrationEventHandler>();
            services.AddTransient<OrdersIntegrationEventHandler>();
        }
        /// <summary>
        /// Uses the swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for UseSwagger
        public void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
            //eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();


            //Với trường hợp chỉ gửi
            //eventBus.InitPublish<OrdersIntegrationEvent, OrdersIntegrationEventHandler>();

            //Trường hợp trong 1 ứng dụng vừa gửi vừa nhận xử lý
            //eventBus.Subscribe<OrdersIntegrationEvent, OrdersIntegrationEventHandler>();

        }
    }
}
