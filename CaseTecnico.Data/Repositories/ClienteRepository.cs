using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public async Task<bool> EmailExistente(string email, int? id = null)
        {
            var query = _dbSet.Where(x => x.Email == email);

            if (id != null)
                query = _dbSet.Where(x => x.Id != id);

            return await query.AnyAsync();
        }

        public async Task<Cliente?> ObterPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
