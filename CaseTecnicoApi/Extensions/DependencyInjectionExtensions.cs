using CaseTecnico.Application.Contracts.Validators;
using CaseTecnico.Application.Services.Clientes;
using CaseTecnico.Data.Database;
using CaseTecnico.Data.Repositories;
using CaseTecnicoApi.Endpoints;
using CaseTecnicoApi.Middlewares;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnicoApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();
        }

        public static void AddDatabaseContexts(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("ClientesDb"));
        }

        public static void AddEndpoints(this IServiceCollection services)
        {
            var endpoints = new List<IEndpoint>();

            endpoints.AddRange(typeof(Program).Assembly.ExportedTypes
                .Where(x => typeof(IEndpoint).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IEndpoint>());

            services.AddSingleton(endpoints as IReadOnlyCollection<IEndpoint>);
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();
        }

        public static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<ExceptionHandlingMiddleware>();
        }
    }
}
