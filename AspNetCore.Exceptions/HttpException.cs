using System;

namespace AspNetCore.ExceptionHandler;

public class HttpException : Exception
{
    public HttpException(int statusCode = 500)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}