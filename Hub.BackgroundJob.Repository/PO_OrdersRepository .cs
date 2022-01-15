using Hub.BackgroundJob.Entities;
using Hub.BackgroundJob.Repository.Base;
using Hub.BackgroundJob.Repository.Interfaces;
using System.Data;

namespace Hub.BackgroundJob.Repository
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
