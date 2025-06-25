using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Services;
using OcelotManagement.Contracts.Common;
using System.Diagnostics;

namespace OcelotManagement.API.Controllers
{
    // 控制器 (接口层)
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SettingBackupController : ControllerBase
    {
        private readonly SettingBackupService _service;
        private readonly ILogger<SettingBackupController> _logger;

        public SettingBackupController(
            SettingBackupService service,
            ILogger<SettingBackupController> logger)
        {
            _service = service;
            _logger = logger;
        }
        public class SettingBackupSearchArg : LayuiPageArg
        {
            public string? ConsulId { get; set; }
            public string? ProjId { get; set; }
            [FromQuery(Name = "rangeTime[]")]
            public string[]? RangeTime { get; set; }

        }
        [HttpGet]
        public async Task<ActionResult<ApiResult<ApiPagedResult<SettingBackupDto>>>> Get([FromQuery] SettingBackupSearchArg arg)
        {
            Domain.Interfaces.SettingBackupSearchArg searchArg = new();
            searchArg.Current = arg.Current;
            searchArg.Limit = arg.Limit;
            searchArg.ProjId = arg.ProjId;
            searchArg.ConsulId = arg.ConsulId;
            if (arg.RangeTime != null && arg.RangeTime.Length > 1)
            {
                if (DateTime.TryParse(arg.RangeTime[0], out var start))
                    searchArg.Start = start;
                if (DateTime.TryParse(arg.RangeTime[1], out var end))
                    searchArg.End = end;
            }
            var backup = await _service.GetAsync(searchArg);
            var result = ApiResult.SuccessResult(backup);
            return Ok(result);
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<SettingBackupDto>> GetById(string id)
        //{
        //    var backup = await _service.GetByIdAsync(id);
        //    if (backup == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(backup);
        //}

        //[HttpGet("project/{projectId}")]
        //public async Task<ActionResult<List<SettingBackupDto>>> GetByProjectId(string projectId)
        //{
        //    var backups = await _service.GetByProjectIdAsync(projectId);
        //    return Ok(backups);
        //}

        //[HttpPost]
        //public async Task<ActionResult<SettingBackupDto>> Create([FromBody] SettingBackupDto dto)
        //{
        //    var createdBackup = await _service.CreateAsync(dto);
        //    return CreatedAtAction(nameof(GetById), new { id = createdBackup.Id }, createdBackup);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] SettingBackupDto dto)
        //{
        //    if (id != dto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _service.UpdateAsync(dto);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await _service.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
