using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.Common
{
    public class Paginator<TData>
    {
        public TData Filter { get; set; }

        const int minPage = 0;
        private int page = 0;
        public int Page
        {
            get { return page; }
            set
            {
                page = (value < minPage) ? minPage : value;
            }
        }

        const int maxPageSize = 10;
        private int count = 3;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
