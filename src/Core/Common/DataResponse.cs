namespace Bainah.CoreApi.Common;
public class DataResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public static DataResponse<T> Ok(T data, string? message = null) => new() { Success = true, Data = data, Message = message };
    public static DataResponse<T> Fail(string message) => new() { Success = false, Message = message };
}
