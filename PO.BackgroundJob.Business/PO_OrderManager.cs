using PO.BackgroundJob.Entities;
using PO.BackgroundJob.Repository.Interfaces;

namespace PO.BackgroundJob.Business
{
    public class PO_OrderManager : PO_BaseManager<PO_Order>, IPO_OrderManager
    {
        IPO_OrderRepository _orderRepository;
        public PO_OrderManager(IPO_OrderRepository orderRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

    }
}
