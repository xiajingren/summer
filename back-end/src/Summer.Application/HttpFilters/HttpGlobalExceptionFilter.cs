using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Summer.Shared.Exceptions;

namespace Summer.Application.HttpFilters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // todo: logo

            switch (context.Exception)
            {
                case ValidationException validationException:
                    var errors = validationException.Errors.Select(p => p.PropertyName)
                        .Distinct()
                        .ToDictionary(propName => propName, propName => validationException.Errors
                            .Where(p => p.PropertyName == propName)
                            .Select(x => x.ErrorMessage)
                            .ToArray());

                    var validationError = new ProblemDetails()
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred.",
                        Detail = "验证错误...",
                        Extensions = {{"errors", errors}}
                    };

                    context.Result = new BadRequestObjectResult(validationError);
                    break;
                case FriendlyException friendlyException:
                    var domainError = new ProblemDetails()
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Business errors occurred.",
                        Detail = friendlyException.Message
                    };

                    context.Result = new BadRequestObjectResult(domainError);
                    break;
                default:
                    var unKnowError = new ProblemDetails()
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Internal server errors occurred.",
                        Detail = "服务器内部错误..."
                    };

                    if (_env.IsDevelopment())
                    {
                        unKnowError.Extensions.Add("exception.message", context.Exception.Message);
                        unKnowError.Extensions.Add("exception.source", context.Exception.Source);
                        unKnowError.Extensions.Add("exception.stackTrace", context.Exception.StackTrace);
                    }

                    context.Result = new InternalServerErrorObjectResult(unKnowError);
                    break;
            }

            context.ExceptionHandled = true;
        }

        private class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}