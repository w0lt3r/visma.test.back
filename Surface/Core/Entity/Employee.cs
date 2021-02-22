using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class Employee : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Phone { get; set; }
    }
}
