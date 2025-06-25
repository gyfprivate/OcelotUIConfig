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
        /// �û���¼
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResult<LoginResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            // ��֤ģ��
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResult<LoginResponseDto>.ErrorResult(400, "��Ч�ĵ�¼��Ϣ"));
            }

            // ִ�е�¼�߼�
            var token = await _authenticationService.AuthenticateAsync(loginDto.Username, loginDto.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(ApiResult<LoginResponseDto>.ErrorResult(401, "�û������������"));
            }

            // ����ͳһ��ʽ�ĳɹ���Ӧ
            return Ok(ApiResult<LoginResponseDto>.SuccessResult(new LoginResponseDto() { Token = token }, "��¼�ɹ�"));
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResult<string>>> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResult<string>.ErrorResult(400, "��Ч���������"));
            }

            var newToken = await _authenticationService.RefreshTokenAsync(dto.Token, dto.RefreshToken);

            if (string.IsNullOrEmpty(newToken))
            {
                return Unauthorized(ApiResult<string>.ErrorResult(401, "ˢ������ʧ��"));
            }

            return Ok(ApiResult<string>.SuccessResult(newToken, "����ˢ�³ɹ�"));
        }

        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<ActionResult<ApiResult<bool>>> RevokeToken()
        {
            var username = User.Identity.Name;
            var result = await _authenticationService.RevokeRefreshTokenAsync(username);

            if (!result)
            {
                return BadRequest(ApiResult<bool>.ErrorResult(400, "��������ʧ��"));
            }

            return Ok(ApiResult<bool>.SuccessResult(true, "�����ѳ���"));
        }
    }
}
