using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Application.Common.Dtos;

public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } 

    public static ApiResponse<T> Success(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Code = (int)ErrorCode.Success,
            Message = message ?? "Success",
            Data = data
        };
    }

    public static ApiResponse<T> Error(string message, ErrorCode errorCode = ErrorCode.BadRequest, T? data = default)
    {
        return new ApiResponse<T>
        {
            Code = (int)errorCode,
            Message = message,
            Data = data
        };
    }
}
