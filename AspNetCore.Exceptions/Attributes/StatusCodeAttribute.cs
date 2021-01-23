using System;

namespace AspNetCore.ExceptionHandler.Attributes
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
