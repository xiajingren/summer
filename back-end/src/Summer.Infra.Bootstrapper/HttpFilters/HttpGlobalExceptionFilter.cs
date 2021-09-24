using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Summer.Infra.Bootstrapper.HttpFilters
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
                case ArgumentException:
                    context.Result = new BadRequestObjectResult(new
                    {
                        Errors = new List<string>() {"参数错误..."}
                    });
                    break;
                default:
                    if (_env.IsDevelopment())
                    {
                        context.Result = new InternalServerErrorResult(new
                        {
                            context.Exception.Message,
                            context.Exception.Source,
                            context.Exception.StackTrace
                        });
                        break;
                    }

                    context.Result = new InternalServerErrorResult(new {Message = "未知错误..."});
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