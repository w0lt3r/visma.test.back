using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class UserRole : Entity<int>
    {
        public string Name { get; set; }
    }
}
