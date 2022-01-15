using Hub.BackgroundJob.Main.IntegrationEvents.Events;
using Hub.BackgroundJob.Repository.Interfaces;
using Hub.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hub.EventBus.Main.IntegrationEvents.EventHandling
{
    public class OrdersIntegrationEventHandler : IIntegrationEventHandler<OrdersIntegrationEvent>
    {
        private readonly ILogger<OrdersIntegrationEventHandler> _logger;
        private readonly IPO_OrdersRepository _ordersRepository;

        public OrdersIntegrationEventHandler(ILogger<OrdersIntegrationEventHandler> logger, IPO_OrdersRepository ordersRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ordersRepository = ordersRepository ?? throw new ArgumentNullException(nameof(ordersRepository));
        }

        public async Task Handle(OrdersIntegrationEvent @event)
        {
            if (!string.IsNullOrWhiteSpace(@event.Method))
            {
                switch (@event.Method)
                {
                    case "POST":
                        break;
                    case "PUT":
                        break;
                    case "DELETE":
                        break;
                }
            }
        }
    }
}

