using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using INDG.GRIP.Trader.Application.Common.Exceptions;
using INDG.GRIP.Trader.Application.Common.Models;

namespace INDG.GRIP.Trader.Presentation.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var message = string.Empty;
            if (context.Exception is ValidationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = ResultBadRequest(((ValidationException)context.Exception).Failures);
                return;
            }

            var code = HttpStatusCode.InternalServerError;

            // if (context.Exception is NotFoundException)
            // {
            //     code = HttpStatusCode.NotFound;
            //     message = context.Exception.Message;
            // }
            //
            // if (context.Exception is ConflictException)
            // {
            //     code = HttpStatusCode.Conflict;
            //     message = context.Exception.Message;
            // }
            //
            // if (context.Exception is InvalidBusinessOperation)
            // {
            //     code = HttpStatusCode.Conflict;
            //     message = context.Exception.Message;
            // }

            Log.Fatal($"CustomExceptionFilterAttribute. code:{(int)code}; Exception: {context.Exception}");
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = ResultException(message, context.Exception.StackTrace, code);
        }
        
        #region private
        private static IActionResult ResultBadRequest(IDictionary<string, string[]> exceptions)
        {
            var ex = new FluentValidationExceptionResponseBody() 
                { Status = StatusCodes.Status400BadRequest};

            foreach (var exp in exceptions)
            {     
                ex.Errors.Add(Utility.ToCamelCase(exp.Key), exp.Value);
            }
            
            return new JsonResult(ex);
        }

        private static IActionResult ResultException(string message, string stackTrace, HttpStatusCode code)
        {
            var responseErrors = new List<string>();
            responseErrors.Add(message);
            var result = new Result<bool>(false, responseErrors.ToArray());
            return new JsonResult(result);
        }
        #endregion
    }
}