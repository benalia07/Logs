﻿namespace Managers.Models;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static ApiResult Success(string message = "Succès.")
        => new ApiResult { IsSuccess = true, Message = message };

    public static ApiResult Failure(string message)
        => new ApiResult { IsSuccess = false, Message = message };
}

public class ApiResult<T> : ApiResult
{
    public T Data { get; set; }

    public static ApiResult<T> Success(T data, string message = "Succès.")
        => new ApiResult<T> { IsSuccess = true, Message = message, Data = data };

    public new static ApiResult<T> Failure(string message)
        => new ApiResult<T> { IsSuccess = false, Message = message, Data = default };
}
