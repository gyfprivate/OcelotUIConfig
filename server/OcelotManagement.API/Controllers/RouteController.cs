using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Common;

namespace OcelotManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RouteController : ControllerBase
    {
        private readonly RouteService _routeService;

        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<RouterListOutputDto>>>> GetRouteDtoList([FromQuery] string projId)
        {
            var data = await _routeService.GetRouteDtoList(projId);
            var result = ApiResult.SuccessResult(data, "获取路由列表成功");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<RouterInfoDto>>> GetRouteDtoById(string id)
        {
            var data = await _routeService.GetRouteDtoById(id);
            if (data == null)
            {
                return NotFound(ApiResult<RouterInfoDto>.ErrorResult(404, "路由未找到"));
            }
            var result = ApiResult.SuccessResult(data, "获取路由成功");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<string>>> AddRoute([FromBody] RouterInfoDto inputDto)
        {
            var data = await _routeService.AddRoute(inputDto);
            var result = ApiResult.SuccessResult(data, "添加路由成功");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<RouterInfoDto>>> UpdateRoute(string id, [FromBody] RouterInfoDto inputDto)
        {
            if (id != inputDto.Id)
            {
                return BadRequest(ApiResult<RouterListOutputDto>.ErrorResult(500, "路由ID不匹配"));
            }
            await _routeService.UpdateRoute(inputDto);

            var result = ApiResult.SuccessResult(inputDto, "更新路由成功");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<bool>>> DeleteRoute(string id)
        {
            var success = await _routeService.DeleteRoute(id);
            if (!success)
            {
                return NotFound(ApiResult<bool>.ErrorResult(404, "路由删除失败"));
            }
            var result = ApiResult.SuccessResult(success, "删除路由成功");
            return Ok(result);
        }

        [HttpPatch("{id}/enabled")]
        public async Task<ActionResult<ApiResult<bool>>> UpdateRouteEnabled(string id, [FromBody] RouterEnabledInputDto input)
        {
            var result = await _routeService.UpdateRouteEnabled(id, input.Enabled);
            if (!result)
            {
                return NotFound(ApiResult<bool>.ErrorResult(404, "没有找到这个数据"));
            }
            return Ok(ApiResult.SuccessResult(true));
        }
    }
}
