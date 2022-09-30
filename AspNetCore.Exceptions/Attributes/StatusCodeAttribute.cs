using System;

namespace AspNetCore.ExceptionHandler.Attributes;

public class StatusCodeAttribute : Attribute
{
    public StatusCodeAttribute(int code)
    {
        StatusCode = code;
    }

    /// <summary>
    ///     The status code of which this exception should return
    /// </summary>
    public int StatusCode { get; }
}