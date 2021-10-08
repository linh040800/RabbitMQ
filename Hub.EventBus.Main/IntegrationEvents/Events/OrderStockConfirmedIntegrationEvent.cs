using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hub.EventBus.Main.IntegrationEvents.Events
{
    public record OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderStockConfirmedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}
