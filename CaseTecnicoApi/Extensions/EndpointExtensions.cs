using CaseTecnicoApi.Endpoints;

namespace CaseTecnicoApi.Extensions
{
    public static class EndpointExtensions
    {
        public static void UseEndpoints(this WebApplication app)
        {
            var endpoints = app.Services.GetRequiredService<IReadOnlyCollection<IEndpoint>>();

            foreach (var module in endpoints)
            {
                module.DefinirRotas(app);
            }
        }
    }
}
