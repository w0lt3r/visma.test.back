using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surface.Model
{
    public class Wrapper<TData>
    {
        public TData Data { get; set; }
        public string Message { get; set; }

        public Wrapper(string message, TData data)
        {
            Data = data;
            Message = message;
        }
        public Wrapper(TData data)
        {
            Data = data;
        }
        public Wrapper(string message)
        {
            Message = message;
        }
    }
}
