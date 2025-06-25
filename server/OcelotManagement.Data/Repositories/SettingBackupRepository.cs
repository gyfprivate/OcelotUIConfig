using OcelotManagement.Contracts.Common;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Data.Entities;
using OcelotManagement.Data.Mappings;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Data.Repositories
{
    internal class SettingBackupRepository : ISettingBackupRepository
    {
        private readonly Repository<tbSettingBak> rep;
        private readonly ISnowflake snow;

        public SettingBackupRepository(Repository<tbSettingBak> rep, ISnowflake snow)
        {
            this.rep = rep;
            this.snow = snow;
        }
        public async Task<string> Add(SettingBackup backup)
        {
            var entity = new tbSettingBak();
            MapperConfig.Map(backup, entity);
            entity.Id = snow.GetSnowID();
            await rep.InsertAsync(entity);
            return entity.Id;
        }

        public void Delete(SettingBackup backup)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiPagedResult<SettingBackup>> GetAsync(SettingBackupSearchArg arg)
        {
            var q = rep.AsQueryable()
                .WhereIF(arg.Start != null && arg.End != null, a => a.BakTime >= arg.Start && a.BakTime < arg.End)
                .WhereIF(!string.IsNullOrEmpty(arg.ProjId), a => a.ProjId == arg.ProjId)
                .WhereIF(!string.IsNullOrEmpty(arg.ConsulId), a => SqlFunc.Subqueryable<tbProject>().Where(s1 => s1.ConfigId == arg.ConsulId && s1.Id == a.ProjId).Any())
                 .Select(a => new SettingBackup()
                 {
                     BackupContent = a.BakJson,
                     BackupTime = a.BakTime,
                     Remark = a.Remark,
                     ProjectId = a.ProjId,
                     Id = a.Id,
                     ProjectName = SqlFunc.Subqueryable<tbProject>().Where(s1 => s1.Id == a.ProjId).Select(s1 => s1.ProjectName),
                     Consul = SqlFunc.Subqueryable<tbConfigCenter>().Where(s2 => SqlFunc.Subqueryable<tbProject>().Where(s1 => s1.Id == a.ProjId && s1.ConfigId == s2.Id).Any()).First(s2 => new ConfigCenter()
                     {
                         Id = s2.Id.SelectAll()
                     })
                 })
                 .MergeTable();

            RefAsync<int> count = new RefAsync<int>();
            var data = await q.ToPageListAsync(arg.Current, arg.Limit, count);

            ApiPagedResult<SettingBackup> result = new();
            result.CurrentPage = arg.Current;
            result.PageSize = arg.Limit;
            result.TotalCount = count;
            result.Items = data;
            result.TotalPages = (int)Math.Ceiling((double)count / arg.Limit);
            return result;
        }

        public Task<List<SettingBackup>> GetByProjectIdAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public void Update(SettingBackup backup)
        {
            throw new NotImplementedException();
        }
    }
}
