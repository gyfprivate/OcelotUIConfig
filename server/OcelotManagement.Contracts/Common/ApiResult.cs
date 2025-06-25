using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotManagement.Contracts.Common
{
    public static class ApiResult
    {
        // 成功返回
        public static ApiResult<T> SuccessResult<T>(T data, string message = "操作成功")
        {
            return new ApiResult<T>
            {
                Code = 200,
                Msg = message,
                Success = true,
                Data = data
            };
        }
    }
    public class ApiResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // 成功返回
        public static ApiResult<T> SuccessResult(T data, string message = "操作成功")
        {
            return new ApiResult<T>
            {
                Code = 200,
                Msg = message,
                Success = true,
                Data = data
            };
        }
    
        // 失败返回
        public static ApiResult<T> ErrorResult(int code, string message)
        {
            return new ApiResult<T>
            {
                Code = code,
                Msg = message,
                Success = false,
                Data = default
            };
        }

        // 分页返回
        public static ApiResult<ApiPagedResult<T>> PagedResult(ApiPagedResult<T> pagedData, string message = "查询成功")
        {
            return new ApiResult<ApiPagedResult<T>>
            {
                Code = 200,
                Msg = message,
                Success = true,
                Data = pagedData
            };
        }
    }
}
