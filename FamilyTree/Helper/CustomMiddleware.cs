using System.Net;
using FamilyTree.Model;
using FamilyTree.Helper.Extension;
using FamilyTree.Model.Enum.ResponseEnum;

namespace FamilyTree.Helper
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody="";

            try
            {
                requestBody = RequestBody(context);
                await _next(context);
            }

            catch (Exception ex)
            {
                var api = context.Request.Scheme + "://" + context.Request.Host + context.Request.Path.ToString().TrimEnd('/');
                api = $"API: ({context.Request.Method}) {api}";

                if (requestBody.NullableIsEmpty() == false) api += " - Body: " + requestBody;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ServiceResponseDTO(ResponseStatusEnum.Failed,ex.Message)
                { ExceptionMessage = api + " ||| " + ex?.InnerException?.ToString() };
                await context.Response.WriteAsync(response.ToJason(true));
            }
        }

        private string? RequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestBodyStream = new StreamReader(context.Request.Body);
            requestBodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var result = requestBodyStream.ReadToEndAsync().Result;
            context.Request.Body.Position = 0;

            return result;
        }
    }
}
