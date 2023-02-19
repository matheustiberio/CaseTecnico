namespace CaseTecnico.Data.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<T> AtualizarAsync(T entidade);

        public Task<bool> ExcluirAsync(T entidade);

        public Task<T> CriarAsync(T entidade);

        public Task<List<T>> ListarAsync();

        public Task<T?> ObterPorIdAsync(int id);
    }
}
