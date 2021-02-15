using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class User : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public UserRole Role { get; set; }
    }
}
