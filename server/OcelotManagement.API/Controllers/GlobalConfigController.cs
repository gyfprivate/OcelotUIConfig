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

    public class GlobalConfigController : ControllerBase
    {
        private readonly GlobalConfigurationService _service;

        public GlobalConfigController(GlobalConfigurationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResult<GlobalConfigurationDto>>> GetById()
        {
            var configuration = await _service.GetByIdAsync("0");
            if (configuration == null)
            {
                return NotFound();
            }
            var result = ApiResult.SuccessResult(configuration);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<GlobalConfigurationDto>>> GetById(string id)
        {
            var configuration = await _service.GetByIdAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }
            var result = ApiResult.SuccessResult(configuration);
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GlobalConfigurationDto>>> GetAll()
        //{
        //    var configurations = await _service.GetAllAsync();
        //    return Ok(configurations);
        //}

        [HttpGet("proj/{projId}")]
        public async Task<ActionResult<IEnumerable<GlobalConfigurationDto>>> GetByProjId(string projId)
        {
            var configurations = await _service.GetByProjIdAsync(projId);
            return Ok(configurations);
        }

        /// <summary>
        /// ProjId为 '0' 表示全局，ID设置为'0'
        /// </summary>
        /// <param name="configurationDto"></param>
        /// <returns></returns>
        [HttpPost("proj/{projId}")]
        public async Task<ActionResult<ApiResult<string>>> Create(string projId, [FromBody] GlobalConfigurationDto configurationDto)
        {
            var id = await _service.CreateAsync(projId, configurationDto);
            var result = ApiResult.SuccessResult(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult<string>>> Update(GlobalConfigurationDto configurationDto)
        {
            await _service.UpdateAsync(configurationDto);
            var result = ApiResult.SuccessResult("更新完毕");
            return Ok(result);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await _service.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
