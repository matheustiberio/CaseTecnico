using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public async Task<Cliente?> ObterPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
