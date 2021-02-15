using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Logging
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _logger = Log.Logger;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (ControlledException ex)
            {
                Log.Fatal(ex, ex.errors[0]);
                await HandleExceptionAsync(context, ex.errors, ex.httpStatusCode);
            }
            catch (Exception ex)
            {
                var completeMessage = GetCompleteExceptionMessage(ex);
                _logger.Error($"Error: - {completeMessage}\n{ex.StackTrace}");
                if (bool.Parse(_configuration.GetSection("ExceptionSettings:ShowCustomMessage").Value) == true)
                    completeMessage = _configuration.GetSection("ExceptionSettings:CustomMessage").Value;
                await HandleExceptionAsync(context, new List<string> { completeMessage }, HttpStatusCode.InternalServerError);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, List<string> errors, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errors.Select(error => new ErrorDetail
            {
                Message = error
            })));
        }
        private string GetCompleteExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;
            var errorMessage = $"{ex.Message}\n{GetCompleteExceptionMessage(ex.InnerException)}";

            return errorMessage;
        }
    }
}

