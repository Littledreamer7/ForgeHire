using ForgeHire.Helpers.Common;
using ForgeHire.Helpers.Common.ForgeHire.Helpers.Common;

namespace ForgeHire.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(ApiResponse.FailureResponse(ex.Message));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = ex.Message,
                    stack = ex.StackTrace
                });
            }
        }
    }
}
