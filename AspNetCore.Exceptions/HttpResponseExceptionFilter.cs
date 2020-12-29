using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ExceptionHandler
{
    public static class HttpResponseExceptionFilterHelpers
    {
        public static IServiceCollection UseExceptionBasedErrorHandling(
            this IServiceCollection services)
        {
            services.AddControllers(options 
                => options.Filters.Add(new HttpResponseExceptionFilter()));
            return services;
        }
    }

    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) 
        {}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            int? statusCode = null;
            object message = null;
            if(context.Exception is IExplainableException explainable)
            {
                message = explainable.Explain();
            }

            if(context.Exception is HttpException exception)
            {
                statusCode = exception.StatusCode;
            } 
            else
            {
                var attribute = context.Exception.GetType()
                    .GetCustomAttributes<StatusCodeAttribute>()
                    .FirstOrDefault();

                if(attribute != null)
                {
                    statusCode = attribute.StatusCode;
                }
            }


            if(statusCode != null)
            {
                context.Result = new ObjectResult(message)
                {
                    StatusCode = statusCode.Value,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
