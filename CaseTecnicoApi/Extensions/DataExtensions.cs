using CaseTecnico.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnicoApi.Extensions
{
    public static class DataExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
        }
    }
}
