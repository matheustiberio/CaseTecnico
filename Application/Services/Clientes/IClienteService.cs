using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Models.Entities;

namespace CaseTecnico.Application.Services.Clientes
{
    public interface IClienteService
    {
        Task<Cliente> AtualizarAsync(int id, AtualizarClienteRequest request);
        Task<Cliente> CriarAsync(CriarClienteRequest request);
        Task<bool> ExcluirAsync(string email);
        Task<List<Cliente>> Listar();
        Task<Cliente?> ObterPorEmailAsync(string email);
    }
}
