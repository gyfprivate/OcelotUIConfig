using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string PassWord { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
