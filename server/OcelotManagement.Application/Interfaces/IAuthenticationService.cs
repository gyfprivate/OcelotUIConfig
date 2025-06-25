using OcelotManagement.Application.Dtos;
using OcelotManagement.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<string> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(string username);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="dto">注册信息</param>
        /// <returns>注册结果</returns>
        Task<ApiResult<bool>> RegisterAsync(RegisterDto dto);

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        Task<bool> IsUsernameExistsAsync(string username);
    }
}
