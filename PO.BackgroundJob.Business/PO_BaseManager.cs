using PO.BackgroundJob.Business.Interfaces;
using PO.BackgroundJob.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PO.BackgroundJob.Business
{
    public class PO_BaseManager<TEntity> : IPO_BaseManager<TEntity> where TEntity : class
    {
        readonly IPO_BaseRepository<TEntity> _baseRepository = null;
        public PO_BaseManager(IPO_BaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Guid> Add(TEntity entity)
        {
            return await _baseRepository.Add(entity);
        }

        public async Task<string> AddBulk(List<TEntity> entity)
        {
            return await _baseRepository.AddBulk(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
            var dataList = await _baseRepository.GetAll();
            return dataList;
        }

        public async Task<List<TEntity>> GetByTenant(Guid? tenantId, bool isRecursive)
        {
            return await _baseRepository.GetByTenant(tenantId, isRecursive);
        }

        public async Task<TEntity> Get(string id)
        {
            return await _baseRepository.Get(id);
        }

        public async Task<List<TEntity>> Get(string code, Guid? tenantId)
        {
            return await _baseRepository.Get(code, tenantId);
        }

        public async Task<bool> Update(TEntity entity)
        {
            return await _baseRepository.Update(entity);
        }

        public async Task<bool> Updates(string sSet, string sWhere)
        {
            return await _baseRepository.Updates(sSet, sWhere);
        }
        public async Task<bool> UpdatesByJsonData(List<TEntity> entityList)
        {
            return await _baseRepository.UpdatesByJsonData(entityList);
        }
        public async Task<bool> Delete(string id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<string> Deletes(string ids)
        {
            ids = ids.TrimEnd(',');
            if (!string.IsNullOrWhiteSpace(ids))
            {
                Regex pattern = new Regex("[,]|[',']{2}");
                ids = pattern.Replace(ids, "','");
            }
            return await _baseRepository.Deletes(string.Format("Id IN ('{0}')", ids));
        }

        public async Task<(List<TEntity>, long)> Paging(string name, int? usedState, int currentPage, int pageSize, Guid? tenantId, Guid? createdBy)
        {
            var dataList = await _baseRepository.Paging(name, usedState, currentPage, pageSize, tenantId, createdBy);
            return dataList;
        }

        public async Task<List<TEntity>> GetParent(string id)
        {
            var dataList = await _baseRepository.GetParent(id);
            return dataList;
        }

        public async Task<bool> IsExisted(string code, Guid? tenantId, Guid? id)
        {
            return await _baseRepository.IsExisted(code, tenantId, id);
        }

        public async Task<List<TEntity>> GetList(string sWhere, string sOrder, int fromRow, int toRow)
        {
            return await _baseRepository.GetList(sWhere, sOrder, fromRow, toRow);
        }
    }
}
