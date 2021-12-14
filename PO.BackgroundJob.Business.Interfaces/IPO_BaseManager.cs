using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO.BackgroundJob.Business.Interfaces
{
    public interface IPO_BaseManager<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetByTenant(Guid? tenantId, bool isRecursive);
        Task<TEntity> Get(string id);
        Task<List<TEntity>> Get(string code, Guid? tenantId);
        Task<Guid> Add(TEntity entity);
        Task<string> AddBulk(List<TEntity> entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Updates(string sSet, string sWhere);
        Task<bool> UpdatesByJsonData(List<TEntity> entityList);
        Task<bool> Delete(string id);
        Task<string> Deletes(string ids);
        Task<(List<TEntity>, long)> Paging(string name, int? usedState, int currentPage, int pageSize, Guid? tenantId, Guid? createdBy);
        Task<List<TEntity>> GetParent(string id);
        Task<bool> IsExisted(string code, Guid? tenantId, Guid? id);
        Task<List<TEntity>> GetList(string sWhere, string sOrder = "", int fromRow = 0, int toRow = 0);
    }
}
