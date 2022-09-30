using System;
using System.Collections.Generic;
using AspNetCore.ExceptionHandler.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace AspNetCore.ExceptionHandler.Tests;

[StatusCode(400)]
public class TestAttributeException : Exception
{
}

public class TestClassException : HttpException, IExplainableException
{
    public TestClassException() : base(400)
    {
    }

    public object Explain()
    {
        return "Test Error explanation";
    }
}

public class HttpResponseExceptionFilterTests
{
    private ActionExecutedContext CreateMock()
    {
        var actionContext = new ActionContext
        {
            HttpContext = new Mock<HttpContext>().Object,
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var context = new ActionExecutedContext(
            actionContext, new List<IFilterMetadata>(), null);
        return context;
    }

    [Fact]
    public void Attribute_Returns_StatusCode()
    {
        var exception = new TestAttributeException();
        var filter = new HttpResponseExceptionFilter();
        var context = CreateMock();
        context.Exception = exception;

        filter.OnActionExecuted(context);

        Assert.NotNull(context.Result as ObjectResult);
        var result = context.Result as ObjectResult;

        Assert.Equal(400, result.StatusCode);
        Assert.Null(result.Value);
    }

    [Fact]
    public void Class_Returns_StatusCode()
    {
        var exception = new TestClassException();
        var filter = new HttpResponseExceptionFilter();
        var context = CreateMock();
        context.Exception = exception;

        filter.OnActionExecuted(context);

        Assert.NotNull(context.Result as ObjectResult);
        var result = context.Result as ObjectResult;

        Assert.Equal(400, result.StatusCode);
        Assert.NotNull(result.Value);
        Assert.Equal(exception.Explain(), result.Value);
    }
}