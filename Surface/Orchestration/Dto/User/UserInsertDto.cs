using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.Dto.User
{
    public class UserInsertDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
