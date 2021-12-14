using Dapper;
using Newtonsoft.Json;
using PO.BackgroundJob.Business.Repository.Base;
using PO.BackgroundJob.Entities.Base;
using PO.BackgroundJob.Repository.Interfaces;
using PO.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static System.Data.CommandType;

namespace PO.BackgroundJob.Business.Repository
{
    public class PO_BaseRepository<TEntity> : IPO_BaseRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly IDbConnection cnn = null;

        public PO_BaseRepository(SqlServerStorage clientDB)
        {
            cnn = clientDB.iDbConnection;
        }

        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_GetAll", commandType: StoredProcedure);
            return result.ToList();
        }

        public async Task<List<TEntity>> GetByTenant(Guid? tenantId, bool isRecursive)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tenantId", tenantId, DbType.Guid);
            parameters.Add("@isRecursive", isRecursive, DbType.Boolean);
            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_GetByTenant", parameters, commandType: StoredProcedure);
            return result.ToList();
        }

        public async Task<TEntity> Get(object id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.String);
            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_GetById", parameters, commandType: StoredProcedure);
            return result.FirstOrDefault();
        }

        public async Task<List<TEntity>> Get(string code, Guid? tenantId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Code", code, DbType.String);
            parameters.Add("@TenantId", tenantId, DbType.Guid);
            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_GetByCode", parameters, commandType: StoredProcedure);
            return (result).ToList();
        }

        public async Task<Guid> Add(TEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(entity);
            parameters.Add("Id", dbType: DbType.Guid, direction: ParameterDirection.ReturnValue);
            var result = await cnn.ExecuteScalarAsync(typeof(TEntity).Name + "_Insert", param: parameters, commandType: CommandType.StoredProcedure);
            return (Guid)result;
        }

        public async Task<string> AddBulk(List<TEntity> entityList)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@XMLDOC", ExtensionXml<TEntity>.ToXml(entityList), DbType.String);
            var result = await cnn.QueryAsync<ModelIdBase>(typeof(TEntity).Name + "_Inserts", param: parameters, commandType: StoredProcedure);
            return result != null ? string.Join(',', result.Select(i => i.Id)) : string.Empty;
        }

        public async Task<bool> Update(TEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(entity);

            var result = await cnn.ExecuteAsync(typeof(TEntity).Name + "_Update", param: parameters, commandType: StoredProcedure);
            return result > 0;
        }

        /// <summary>
        /// Recommendation: Not recommended for API. This code is not safe, maybe SQL Inject
        /// </summary>
        /// <param name="sSet"></param>
        /// <param name="sWhere"></param>
        /// <returns></returns>
        public async Task<bool> Updates(string sSet, string sWhere)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sSet", sSet);
            parameters.Add("@sWhere", sWhere);
            var result = await cnn.ExecuteScalarAsync(typeof(TEntity).Name + "_Updates", param: parameters, commandType: StoredProcedure);
            return (int)result > 0;
        }

        public async Task<bool> UpdatesByJsonData(List<TEntity> entityList)
        {
            var jsonFormat = new JsonSerializerSettings
            {
                DateFormatString = "yyyy/MM/dd hh:mm:ss",
                Formatting = Formatting.Indented
            };
            var jsonData = JsonConvert.SerializeObject(entityList, jsonFormat);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@jsondata", jsonData);
            try
            {
                var result = await cnn.ExecuteAsync($"{typeof(TEntity).Name}_{nameof(UpdatesByJsonData)}", param: parameters, commandType: StoredProcedure);
                return result >= 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await cnn.ExecuteAsync(typeof(TEntity).Name + "_Delete", param: parameters, commandType: StoredProcedure);
            return result > 0;
        }
        /// <summary>
        /// Recommendation: Not recommended for API. This code is not safe, maybe SQL Inject
        /// </summary>
        /// <param name="sWhere"></param>
        /// <returns></returns>
        public async Task<string> Deletes(string sWhere)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sWhere", sWhere);
            var result = await cnn.QueryAsync<ModelIdBase>(typeof(TEntity).Name + "_Deletes", param: parameters, commandType: StoredProcedure);
            return result != null ? string.Join(',', result.Select(i => i.Id)) : string.Empty;
        }

        public async Task<(List<TEntity>, long)> Paging(string name, int? usedState, int currentPage, int pageSize, Guid? tenantId, Guid? createdBy)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@name", name);
            parameters.Add("@usedState", usedState);
            parameters.Add("@currentPage", currentPage);
            parameters.Add("@rowsInpage", pageSize);
            parameters.Add("@tenantId", tenantId, DbType.Guid);
            parameters.Add("@createdBy", createdBy, DbType.Guid);
            parameters.Add("@totalRows", pageSize, DbType.Int64, ParameterDirection.Output);

            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_Paging", parameters, commandType: StoredProcedure);
            long totalRows = parameters.Get<long>("@totalRows");

            return (result.ToList(), totalRows);
        }

        public async Task<List<TEntity>> GetParent(object id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@parentId", id, DbType.String);
            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_GetByParent", parameters, commandType: StoredProcedure);
            return result.ToList();
        }

        public async Task<bool> IsExisted(string code, Guid? tenantId = null, Guid? id = null)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tenantId", tenantId, DbType.Guid);
            parameters.Add("@id", id, DbType.Guid);
            parameters.Add("@code", code);
            parameters.Add("@isExisted", false, DbType.Boolean, ParameterDirection.Output);
            await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_IsCodeExisted", param: parameters, commandType: StoredProcedure);
            return parameters.Get<Boolean>("@isExisted");
        }

        public async Task<List<TEntity>> GetList(string sWhere, string sOrder, int fromRow, int toRow)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@sWhere", sWhere);
            parameters.Add("@sOrder", sOrder);
            parameters.Add("@fromRow", fromRow);
            parameters.Add("@toRow", toRow);
            var result = await cnn.QueryAsync<TEntity>(typeof(TEntity).Name + "_List", parameters, commandType: StoredProcedure);
            return result.ToList();
        }

    }
}
