using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    public class LoginResponseDto
    {
        /// <summary>
        /// JWT令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDto Info { get; set; }
    }
    public class UserInfoDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
