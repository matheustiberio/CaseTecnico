using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Data.Repositories;
using CaseTecnico.Domain.ResultObject;

namespace CaseTecnico.Application.Services.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<Cliente>> AtualizarAsync(int id, AtualizarClienteRequest request)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);

            if (cliente == null)
                return Result<Cliente>.FailureResult(MensagensErro.ClienteNaoEncontrado);

            if (await _clienteRepository.EmailExistente(request.Email, id))
                return Result<Cliente>.FailureResult(MensagensErro.EmailJaUtilizado);

            cliente.NomeCompleto = request.NomeCompleto;
            cliente.Email = request.Email;

            return await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task<Result<Cliente>> CriarAsync(CriarClienteRequest request)
        {
            if (await _clienteRepository.EmailExistente(request.Email))
                return Result<Cliente>.FailureResult(MensagensErro.EmailJaUtilizado);

            var novoCliente = new Cliente(request.NomeCompleto, request.Email);

            return await _clienteRepository.CriarAsync(novoCliente);
        }

        public async Task<Result<bool>> ExcluirAsync(string email)
        {
            var cliente = await _clienteRepository.ObterPorEmailAsync(email);

            if (cliente == null)
                return Result<bool>.FailureResult(MensagensErro.ClienteNaoEncontrado);

            return await _clienteRepository.ExcluirAsync(cliente);
        }

        public async Task<Result<List<Cliente>>> Listar()
        {
            return await _clienteRepository.ListarAsync();
        }

        public async Task<Result<Cliente>> ObterPorEmailAsync(string email)
        {
            var cliente = await _clienteRepository.ObterPorEmailAsync(email);
            
            if (cliente == null)
                return Result<Cliente>.FailureResult(MensagensErro.ClienteNaoEncontrado);
            
            return cliente;
        }
    }
}
