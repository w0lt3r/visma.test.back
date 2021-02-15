using System;
using System.Collections.Generic;
using System.Net;

namespace Logging
{
    public class ControlledException : Exception
    {
        public List<string> errors { get; private set; }
        public HttpStatusCode httpStatusCode { get; private set; }

        public ControlledException(List<string> errors, HttpStatusCode httpStatusCode)
           : base(errors[0])
        {
            this.errors = errors;
            this.httpStatusCode = httpStatusCode;
        }
    }
}
