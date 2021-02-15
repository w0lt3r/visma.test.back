using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public class Entity<PrimaryKeyClass>
    {
        public PrimaryKeyClass Id { get; set; }
        public bool Active { get; set; }
    }
}
