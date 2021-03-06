using PO.BackgroundJob.Entities;
using PO.BackgroundJob.Repository.Interfaces;

namespace PO.BackgroundJob.Business
{
    public class PO_OrdersManager : PO_BaseManager<PO_Orders>, IPO_OrdersManager
    {
        IPO_OrdersRepository _ordersRepository;
        public PO_OrdersManager(IPO_OrdersRepository ordersRepository) : base(ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

    }
}
