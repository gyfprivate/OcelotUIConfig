using OcelotManagement.Application.Dtos;
using OcelotManagement.Application.Interfaces;
using OcelotManagement.Contracts.Common;
using OcelotManagement.Contracts.Interfaces;
using OcelotManagement.Domain.Entities;
using OcelotManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            User user = await _userRepository.VerifyPassword(username, password);
            if (user == null || user.PassWord != password)
            {
                return null;
            }

            return _jwtService.GenerateToken(user.Username, user.Role);
        }

        //private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    using (var hmac = new HMACSHA512(storedSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != storedHash[i])
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        public async Task<string> RefreshTokenAsync(string token, string refreshToken)
        {
            // 验证JWT令牌
            var principal = _jwtService.ValidateToken(token);
            if (principal == null)
            {
                return null;
            }

            // 从令牌中获取用户名
            var username = principal.Identity.Name;

            // 获取存储的刷新令牌
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            // 验证刷新令牌
            if (storedRefreshToken == null ||
                storedRefreshToken.Username != username ||
                storedRefreshToken.Expires < DateTime.UtcNow)
            {
                return null;
            }

            // 生成新的JWT令牌
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            // 更新刷新令牌（可选：每次刷新都生成新的刷新令牌）
            storedRefreshToken.Expires = DateTime.UtcNow.AddDays(7);
            await _refreshTokenRepository.UpdateAsync(storedRefreshToken);

            // 生成新的访问令牌
            return _jwtService.GenerateToken(username, user.Role);
        }

        public async Task<bool> RevokeRefreshTokenAsync(string username)
        {
            // 删除用户的所有刷新令牌
            return await _refreshTokenRepository.RevokeByUsernameAsync(username);
        }


        public async Task<ApiResult<bool>> RegisterAsync(RegisterDto dto)
        {
            // 验证用户名是否存在
            if (await IsUsernameExistsAsync(dto.Username))
            {
                return ApiResult<bool>.ErrorResult(400, "用户名已存在");
            }

            //// 验证邮箱是否存在
            //if (await IsEmailExistsAsync(dto.Email))
            //{
            //    return ApiResult<bool>.ErrorResult(400, "邮箱已被注册");
            //}

            // 创建用户实体
            var user = new User
            {
                Username = dto.Username,
                PassWord = dto.Password,
            };

            // 保存用户到数据库
            var result = await _userRepository.AddAsync(user);

            if (!result)
            {
                return ApiResult<bool>.ErrorResult(500, "注册失败，请重试");
            }

            return ApiResult<bool>.SuccessResult(true, "注册成功");
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user != null;
        }

    }
}

