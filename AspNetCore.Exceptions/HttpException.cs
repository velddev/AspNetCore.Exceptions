using System;

namespace AspNetCore.ExceptionHandler
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }

        public HttpException(int statusCode = 500)
        {
            StatusCode = statusCode;
        }
    }
}
