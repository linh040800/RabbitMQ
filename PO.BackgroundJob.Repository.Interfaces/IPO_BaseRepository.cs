using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO.BackgroundJob.Repository.Interfaces
{
    public interface IPO_BaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetByTenant(Guid? tenantId, bool isRecursive);
        Task<TEntity> Get(object id);
        Task<List<TEntity>> Get(string code, Guid? tenantId);
        Task<Guid> Add(TEntity entity);
        Task<string> AddBulk(List<TEntity> entityList);
        Task<bool> Update(TEntity entity);
        Task<bool> Updates(string sSet, string sWhere);
        Task<bool> UpdatesByJsonData(List<TEntity> entityList);
        Task<bool> Delete(string id);
        Task<string> Deletes(string sWhere);
        Task<(List<TEntity>, long)> Paging(string name, int? usedState, int currentPage, int pageSize, Guid? tenantId, Guid? createdBy);
        Task<List<TEntity>> GetParent(object id);
        Task<bool> IsExisted(string code, Guid? tenantId, Guid? id);
        Task<List<TEntity>> GetList(string sWhere, string sOrder, int fromRow, int toRow);
    }
}
