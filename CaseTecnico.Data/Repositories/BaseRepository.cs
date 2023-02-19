using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CaseTecnico.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public readonly DatabaseContext _databaseContext;
        public readonly DbSet<T> _dbSet;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }

        public async Task<T> AtualizarAsync(T entidade)
        {
            _dbSet.Update(entidade);

            await _databaseContext.SaveChangesAsync();

            return entidade;
        }

        public async Task<bool> ExcluirAsync(T entidade)
        {
            _dbSet.Remove(entidade);

            int linhasAfetadas = await _databaseContext.SaveChangesAsync();

            return linhasAfetadas > 0;
        }

        public async Task<T> CriarAsync(T entidade)
        {
            _dbSet.Add(entidade);

            await _databaseContext.SaveChangesAsync();

            return entidade;
        }

        public async Task<List<T>> ListarAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> ObterPorIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
