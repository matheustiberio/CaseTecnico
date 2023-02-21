using CaseTecnico.Application.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.Data.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
