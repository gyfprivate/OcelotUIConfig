using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Interfaces;
using OcelotManagement.Contracts.Common;

namespace OcelotManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResult<LoginResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            // 验证模型
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResult<LoginResponseDto>.ErrorResult(400, "无效的登录信息"));
            }

            // 执行登录逻辑
            var token = await _authenticationService.AuthenticateAsync(loginDto.Username, loginDto.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(ApiResult<LoginResponseDto>.ErrorResult(401, "用户名或密码错误"));
            }

            // 返回统一格式的成功响应
            return Ok(ApiResult<LoginResponseDto>.SuccessResult(new LoginResponseDto() { Token = token }, "登录成功"));
        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResult<string>>> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResult<string>.ErrorResult(400, "无效的请求参数"));
            }

            var newToken = await _authenticationService.RefreshTokenAsync(dto.Token, dto.RefreshToken);

            if (string.IsNullOrEmpty(newToken))
            {
                return Unauthorized(ApiResult<string>.ErrorResult(401, "刷新令牌失败"));
            }

            return Ok(ApiResult<string>.SuccessResult(newToken, "令牌刷新成功"));
        }

        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<ActionResult<ApiResult<bool>>> RevokeToken()
        {
            var username = User.Identity.Name;
            var result = await _authenticationService.RevokeRefreshTokenAsync(username);

            if (!result)
            {
                return BadRequest(ApiResult<bool>.ErrorResult(400, "撤销令牌失败"));
            }

            return Ok(ApiResult<bool>.SuccessResult(true, "令牌已撤销"));
        }
    }
}
