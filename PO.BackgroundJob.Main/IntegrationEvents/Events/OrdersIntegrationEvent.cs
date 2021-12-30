using PO.EventBus.Events;
using System;

namespace PO.BackgroundJob.Main.IntegrationEvents.Events
{
    public record OrdersIntegrationEvent: IntegrationEvent
    {
        //public int ProductId { get; private init; }
        public string OrderCode { get; private init; }
        public DateTime OrderTime { get; private init; }
        public DateTime? OrderTimeTo { get; private init; }
        public DateTime? DateOfIssue { get; private init; }

        public OrdersIntegrationEvent(string orderCode, DateTime orderTime, DateTime? orderTimeTo, DateTime? dateOfIssue,string method)
        {
            OrderCode = orderCode;
            OrderTime = orderTime;
            OrderTimeTo = orderTimeTo;
            DateOfIssue = dateOfIssue;
            Method = method;
        }
    }
}
