using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Summer.Domain.Exceptions;

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
                case DomainException domainException:
                    var domainError = new ProblemDetails()
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Business errors occurred.",
                        Detail = domainException.Message
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

                    context.Result = new InternalServerErrorResult(unKnowError);
                    break;
            }

            context.ExceptionHandled = true;
        }

        private class InternalServerErrorResult : ObjectResult
        {
            public InternalServerErrorResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

    }
}