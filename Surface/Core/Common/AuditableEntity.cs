using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public class AuditableEntity<PrimaryKeyClass> : Entity<PrimaryKeyClass>
    {
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
