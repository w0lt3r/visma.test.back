using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.Dto.User
{
    public class UserFilterDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Role { get; set; }
        public bool? Active { get; set; }
    }
}
