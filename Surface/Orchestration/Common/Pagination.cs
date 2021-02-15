using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.Common
{
    public class Pagination<TData>
    {
        public int TotalCount { get; set; }
        public List<TData> Elements { get; set; }
    }
}
