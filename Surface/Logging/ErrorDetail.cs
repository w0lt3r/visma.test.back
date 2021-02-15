using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging
{
    public class ErrorDetail
    {
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
