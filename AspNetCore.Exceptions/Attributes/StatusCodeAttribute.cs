using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.ExceptionHandler
{
    public class StatusCodeAttribute : Attribute
    {
        /// <summary>
        /// The status code of which this exception should return
        /// </summary>
        public int StatusCode { get; }

        public StatusCodeAttribute(int code)
        {
            StatusCode = code;
        }
    }
}
