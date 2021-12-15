using PO.BackgroundJob.Entities;
using PO.BackgroundJob.Repository.Base;
using PO.BackgroundJob.Repository.Interfaces;
using System.Data;

namespace PO.BackgroundJob.Repository
{
    public class PO_OrdersRepository : PO_BaseRepository<PO_Orders>, IPO_OrdersRepository
    {
        private readonly IDbConnection cnn = null;
        public PO_OrdersRepository(SqlServerStorage clientDB): base(clientDB)
        {
            cnn = clientDB.iDbConnection;
        }

        
    }
}
