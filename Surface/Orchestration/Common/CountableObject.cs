using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.Common
{
    public class CountableObject<TData>
    {
        public int Count { get; set; }
        public TData Data { get; set; }
    }
}
