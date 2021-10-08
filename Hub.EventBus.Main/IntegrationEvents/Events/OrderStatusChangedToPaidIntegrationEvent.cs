using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hub.EventBus.Main.IntegrationEvents.Events
{
    public record OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToPaidIntegrationEvent(int orderId,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }
}
