using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Application.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "用户名长度必须在3-50个字符之间")]
        public string Username { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密码长度必须至少6个字符")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "邮箱不能为空")]
        //[EmailAddress(ErrorMessage = "邮箱格式不正确")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "角色不能为空")]
        //[RegularExpression("^(Admin|User|ReadOnly)$", ErrorMessage = "角色必须是Admin、User或ReadOnly")]
        //public string Role { get; set; } = "User";
    }
}
