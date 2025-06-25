
using Microsoft.AspNetCore.Authorization;

// 在 API 层创建控制器
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Mappings;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OcelotManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConfigCenterController : ControllerBase
    {
        private readonly ConfigCenterService _service;

        public ConfigCenterController(ConfigCenterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<string>>> Add([FromBody] ConfigCenterInputDto arg)
        {

            var id = await _service.AddAsync(arg);
            ApiResult<string> result = ApiResult<string>.SuccessResult(id, "配置中心添加成功");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<string>>> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return Ok(ApiResult.SuccessResult("删除成功"));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<IEnumerable<ConfigCenterDto>>>> GetAll()
        {
            var configCenters = await _service.GetAllAsync();
            return Ok(ApiResult<IEnumerable<ConfigCenterDto>>.SuccessResult(configCenters));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigCenterDto>> GetById(string id)
        {
            var configCenter = await _service.GetByIdAsync(id);
            if (configCenter == null)
            {
                return NotFound();
            }
            return Ok(configCenter);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<string>>> Update(string id, [FromBody] ConfigCenterInputDto configCenter)
        {
            ConfigCenterDto data = MapperConfig.Map<ConfigCenterDto>(configCenter);
            data.Id = id;
            await _service.UpdateAsync(data);
            var result = ApiResult<string>.SuccessResult("更新成功");
            return Ok(result);
        }
    }
}

