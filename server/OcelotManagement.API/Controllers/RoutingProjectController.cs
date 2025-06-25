using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;

namespace OcelotManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoutingProjectController : ControllerBase
    {
        private const int TYPE = 2;
        private readonly ProjectService _projectService;

        public RoutingProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<ApiPagedResult<ProjDto>>>> GetAll([FromQuery] ApiPageArg arg)
        {
            var projects = await _projectService.GetByTypeAsync(TYPE, arg);
            var result = ApiResult<ApiPagedResult<ProjDto>>.SuccessResult(projects, "获取路由项目列表成功");
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ApiResult<ProjDto>>> GetById(string id)
        //{
        //    var project = await _projectService.GetByIdAsync(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }
        //    var result = ApiResult<ProjDto>.SuccessResult(project, "获取同步项目成功");
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<ActionResult<ApiResult<string>>> Create(ProjInputDto project)
        {
            var id = await _projectService.AddAsync(project, TYPE);
            var result = ApiResult<string>.SuccessResult(id, "创建同步项目成功");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<string>>> Update(string id, ProjInputDto project)
        {
            if (string.IsNullOrEmpty(id) || id == "0")
            {
                return BadRequest();
            }
            ProjDto data = MapperConfig.Map<ProjDto>(project);
            data.Id = id;
            await _projectService.UpdateAsync(data);
            var result = ApiResult<string>.SuccessResult(id, "更新同步项目成功");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<string>>> Delete(string id)
        {
            await _projectService.DeleteAsync(id);
            var result = ApiResult<string>.SuccessResult(id, "删除同步项目成功");
            return Ok(result);
        }
    }
}
