using CaseTecnico.Domain.ResultObject;
using System.Net;
using System.Text.Json;

namespace CaseTecnicoApi.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                await context.Response.WriteAsJsonAsync(MensagensErro.ErroInterno);
            }
        }
    }
}
