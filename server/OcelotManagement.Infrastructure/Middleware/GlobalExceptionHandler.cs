using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OcelotManagement.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OcelotManagement.Infrastructure.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "全局异常捕获: {Message}", ex.Message);

            var code = HttpStatusCode.InternalServerError;
            var message = "系统内部错误，请联系管理员";

            // 根据异常类型设置不同的返回码和消息
            if (ex is ArgumentException)
            {
                code = HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            else if (ex is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
                message = "未授权访问";
            }
            else if (ex is KeyNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                message = ex.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var result = JsonSerializer.Serialize(ApiResult<object>.ErrorResult((int)code, message));
            return context.Response.WriteAsync(result);
        }
    }
}
