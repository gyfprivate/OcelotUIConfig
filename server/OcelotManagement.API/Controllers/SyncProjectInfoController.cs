using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;

namespace OcelotManagement.API.Controllers
{
    /// <summary>
    /// 项目详情相关数据
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SyncProjectInfoController : ControllerBase
    {
        private readonly ProjInfoService _projectService;

        public SyncProjectInfoController(ProjInfoService projectService)
        {
            this._projectService = projectService;
        }
  

        [HttpPost("config")]
        public async Task<ActionResult<ApiResult<string>>> SaveConfig([FromBody] ProjConfigSaveDto project)
        {
            await _projectService.SaveConfig(project);
            var result = ApiResult<string>.SuccessResult("保存成功", "保存项目配置成功");
            return Ok(result);
        }

        [HttpPost("routes")]
        public async Task<ActionResult<ApiResult<string>>> SaveRoutes([FromBody] ProjRouteSaveDto project)
        {
            await _projectService.SaveRoutes(project);
            var result = ApiResult<string>.SuccessResult("保存成功", "保存路由配置成功");
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<SyncProjInfoDto>>> GetById(string id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            var result = ApiResult<SyncProjInfoDto>.SuccessResult(project, "获取项目详情成功");
            return Ok(result);
        }

        
    }
}
