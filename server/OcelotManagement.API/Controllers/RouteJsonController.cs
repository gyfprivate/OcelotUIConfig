using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Dtos.Ocelot;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;

namespace OcelotManagement.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RouteJsonController : ControllerBase
    {
        private readonly RouteService routeServer;
        private readonly ConfigurationSyncService syncServer;
        private readonly SettingBackupService backupService;

        public RouteJsonController(RouteService routeServer, ConfigurationSyncService syncServer, SettingBackupService backupService)
        {
            this.routeServer = routeServer;
            this.syncServer = syncServer;
            this.backupService = backupService;
        }

        /// <summary>
        /// 同步更新到Consul KV
        /// </summary>
        /// <param name="projId"></param>
        /// <returns></returns>
        [HttpPost("{projId}")]
        public async Task<ActionResult<ApiResult<string>>> Sync(string projId)
        {
            await syncServer.SyncConfigurationsToConsulAsync(projId);

            var result = ApiResult<string>.SuccessResult("同步项目成功");
            return Ok(result);
        }

        /// <summary>
        /// 回滚到指定版本的配置信息，通过指定备份ID来回滚到之前的某个状态。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<string>>> RollBack(string id)
        {
            await syncServer.RollBackConfigurationsAsync(id);

            var result = ApiResult<string>.SuccessResult("回滚项目成功");
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<OcelotConfigurationDto>>> GetById(string id)
        {
            var json = await syncServer.GetConfigurationByProjIdAsync(id);
            var result = ApiResult.SuccessResult(json);
            return Ok(result);
        }
    }
}
