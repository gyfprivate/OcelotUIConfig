using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using OcelotManagement.Contracts.Common;

namespace OcelotManagement.API.Filters
{
    public class ApiResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // 处理控制器返回值
            switch (context.Result)
            {
                case OkObjectResult okResult:
                    context.Result = new OkObjectResult(ApiResult<object>.SuccessResult(okResult.Value));
                    break;
                case OkResult:
                    context.Result = new OkObjectResult(ApiResult<object>.SuccessResult(null));
                    break;
                case CreatedResult createdResult:
                    context.Result = new CreatedResult(createdResult.Location,
                        ApiResult<object>.SuccessResult(createdResult.Value));
                    break;
                case NoContentResult:
                    context.Result = new OkObjectResult(ApiResult<object>.SuccessResult(null));
                    break;
                case BadRequestObjectResult badRequestResult:
                    if (badRequestResult.Value is ValidationProblemDetails details)
                    {
                        var errorMessage = string.Join("; ", details.Errors.SelectMany(x => x.Value));
                        context.Result = new BadRequestObjectResult(ApiResult<object>.ErrorResult(400, errorMessage));
                    }
                    else
                    {
                        context.Result = new BadRequestObjectResult(ApiResult<object>.ErrorResult(400, badRequestResult.Value?.ToString() ?? "参数错误"));
                    }
                    break;
                case NotFoundResult:
                    context.Result = new NotFoundObjectResult(ApiResult<object>.ErrorResult(404, "资源不存在"));
                    break;
                case UnauthorizedObjectResult:
                    context.Result = new UnauthorizedObjectResult(ApiResult<object>.ErrorResult(401, "未授权"));
                    break;
                case ForbidResult:
                    context.Result = new ObjectResult(ApiResult<object>.ErrorResult(403, "禁止访问"))
                    {
                        StatusCode = 403
                    };
                    break;
            }

            await next();
        }
    }
}
