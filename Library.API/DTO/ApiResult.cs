using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.DTO
{
    public class ApiResult
    {
        public bool IsWarn { get; set; }
        public bool IsError { get; set; }

        public static ApiResult<T> Success<T>(T data)
        {
            return new ApiResult<T> { Data = data, IsError = false, IsWarn = false };
        }

        public static ApiResult Failed()
        {
            return new ApiResult { IsWarn = false, IsError = true };
        }

        public static ApiResult<T> Failed<T>()
        {
            return new ApiResult<T> { Data = default,  IsWarn = false, IsError = true };
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
    }
}
