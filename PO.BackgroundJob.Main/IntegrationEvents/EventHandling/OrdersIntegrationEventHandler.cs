using PO.EventBus.Abstractions;
using PO.EventBus.Main.IntegrationEvents.Events;
using PO.EventBus.Main.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using PO.BackgroundJob.Main.IntegrationEvents.Events;
using PO.BackgroundJob.Repository.Interfaces;

namespace PO.EventBus.Main.IntegrationEvents.EventHandling
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

