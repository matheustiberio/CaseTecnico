using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Domain.ResultObject;

namespace CaseTecnico.Application.Services.Clientes
{
    public interface IClienteService
    {
        Task<Result<Cliente>> AtualizarAsync(int id, AtualizarClienteRequest request);
        Task<Result<Cliente>> CriarAsync(CriarClienteRequest request);
        Task<Result<bool>> ExcluirAsync(string email);
        Task<Result<List<Cliente>>> Listar();
        Task<Result<Cliente>> ObterPorEmailAsync(string email);
    }
}
