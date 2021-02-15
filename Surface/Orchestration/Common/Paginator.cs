using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.Common
{
    public class Paginator<TData>
    {
        public TData Filter { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
    }
}
