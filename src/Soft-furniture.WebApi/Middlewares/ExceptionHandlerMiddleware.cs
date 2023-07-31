using Newtonsoft.Json;
using Soft_furniture.Domain.Exceptions;

namespace Soft_furniture.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _request;

        public ExceptionHandlerMiddleware(RequestDelegate request)
        {
            this._request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch(ClientException exception )
            {
                var obj = new
                {
                    StatusCode = (int)exception.StatusCode,
                    ErrorMessage = exception.TitleMessage
                };
                context.Response.StatusCode = (int)exception.StatusCode;
                context.Response.Headers.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(obj);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
