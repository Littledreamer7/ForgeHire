namespace ForgeHire.Helpers.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ApiResponse(bool success, string message, object? data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse SuccessResponse(string message, object? data = null)
            => new ApiResponse(true, message, data);

        public static ApiResponse FailureResponse(string message)
            => new ApiResponse(false, message);
    }
}