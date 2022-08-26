using CleanArchitecture.API.Errors;
using System.Net;

namespace CleanArchitecture.API.Middleware
{
    public class ExcepcionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExcepcionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExcepcionMiddleware(RequestDelegate next, ILogger<ExcepcionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var respoonse = env.IsDevelopment()
                    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);

                throw;
            }
        }
    }
}
