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

        public static void AddDatabaseContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var(connectionString, mySqlServerVersion) = GetDatabaseConnection(configuration);

            services.AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, mySqlServerVersion));
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
        
        private static (string ConnectionString, MySqlServerVersion MySqlVersion) GetDatabaseConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySQL");
            var databaseVersion = configuration.GetSection("ConnectionStrings").GetSection("Version").Value;

            int majorVersion = Convert.ToInt32(databaseVersion.Split(".")[0]);
            int minorVersion = Convert.ToInt32(databaseVersion.Split(".")[2]);

            return (connectionString, new(new Version(majorVersion, minorVersion)));
        }
    }
}
