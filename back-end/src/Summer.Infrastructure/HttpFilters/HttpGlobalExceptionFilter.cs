using System;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Summer.Domain.Exceptions;

namespace Summer.Infrastructure.HttpFilters
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

            context.Result = context.Exception switch
            {
                ValidationException validationException => CreateResult(context.HttpContext.Request.Path,
                    validationException),
                UnauthorizedBusinessException unauthorizedBusinessException => CreateResult(
                    context.HttpContext.Request.Path, unauthorizedBusinessException),
                ForbidBusinessException forbidBusinessException => CreateResult(context.HttpContext.Request.Path,
                    forbidBusinessException),
                NotFoundBusinessException notFoundBusinessException => CreateResult(context.HttpContext.Request.Path,
                    notFoundBusinessException),
                ErrorsBusinessException errorsBusinessException => CreateResult(context.HttpContext.Request.Path,
                    errorsBusinessException),
                DetailErrorsBusinessException detailErrorsBusinessException => CreateResult(
                    context.HttpContext.Request.Path, detailErrorsBusinessException),
                BusinessException businessException =>
                    CreateResult(context.HttpContext.Request.Path, businessException),
                _ => CreateResult(context.HttpContext.Request.Path, context.Exception)
            };

            context.ExceptionHandled = true;
        }

        private ActionResult CreateResult(string path, ValidationException exception)
        {
            // var errors = exception.Errors.Select(p => p.PropertyName)
            //     .Distinct()
            //     .ToDictionary(propName => propName, propName => exception.Errors
            //         .Where(p => p.PropertyName == propName)
            //         .Select(x => x.ErrorMessage)
            //         .ToArray());

            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "One or more validation errors occurred.",
                Detail = "验证错误，详情请见：ValidationErrors",
                Extensions = { { "validationErrors", exception.Errors } }
            };

            return new BadRequestObjectResult(problemDetails);
        }
        
        private ActionResult CreateResult(string path, UnauthorizedBusinessException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Unauthorized.",
                Detail = exception.Message
            };

            return new UnauthorizedObjectResult(problemDetails);
        }

        private ActionResult CreateResult(string path, ForbidBusinessException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Forbidden.",
                Status = StatusCodes.Status403Forbidden,
                Detail = exception.Message
            };

            return new BadRequestObjectResult(problemDetails);
        }
        
        private ActionResult CreateResult(string path, NotFoundBusinessException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Resource not found.",
                Detail = exception.Message
            };

            return new NotFoundObjectResult(problemDetails);
        }

        private ActionResult CreateResult(string path, ErrorsBusinessException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Business errors occurred.",
                Detail = exception.Message,
                Extensions = { { "errors", exception.Errors } }
            };

            return new BadRequestObjectResult(problemDetails);
        }

        private ActionResult CreateResult(string path, DetailErrorsBusinessException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Business errors occurred.",
                Detail = exception.Message,
                Extensions = { { "detailErrors", exception.DetailErrors } }
            };

            return new BadRequestObjectResult(problemDetails);
        }

        private ActionResult CreateResult(string path, BusinessException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Business errors occurred.",
                Detail = exception.Message
            };

            return new BadRequestObjectResult(problemDetails);
        }

        private ActionResult CreateResult(string path, Exception exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = path,
                Title = "Internal server errors occurred.",
                Detail = "服务器内部错误..."
            };

            if (!_env.IsDevelopment())
                return new InternalServerErrorObjectResult(problemDetails);

            problemDetails.Extensions.Add("exception.message", exception.Message);
            problemDetails.Extensions.Add("exception.source", exception.Source);
            problemDetails.Extensions.Add("exception.stackTrace", exception.StackTrace);

            return new InternalServerErrorObjectResult(problemDetails);
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