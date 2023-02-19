using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Data.Repositories;

namespace CaseTecnico.Application.Services.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> AtualizarAsync(int id, AtualizarClienteRequest request)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            if (await _clienteRepository.EmailExistente(request.Email, id))
                throw new Exception("Este e-mail já está em uso.");

            cliente.NomeCompleto = request.NomeCompleto;
            cliente.Email = request.Email;

            return await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task<Cliente> CriarAsync(CriarClienteRequest request)
        {
            if (await _clienteRepository.EmailExistente(request.Email))
                throw new Exception("Este e-mail já está em uso.");

            var novoCliente = new Cliente(request.NomeCompleto, request.Email);

            return await _clienteRepository.CriarAsync(novoCliente);
        }

        public async Task<bool> ExcluirAsync(string email)
        {
            var cliente = await _clienteRepository.ObterPorEmailAsync(email);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            var clienteExcluido = await _clienteRepository.ExcluirAsync(cliente);

            if (!clienteExcluido)
                throw new Exception("Erro ao excluir o cliente.");

            return clienteExcluido;
        }

        public async Task<List<Cliente>> Listar()
        {
            return await _clienteRepository.ListarAsync();
        }

        public async Task<Cliente?> ObterPorEmailAsync(string email)
        {
            return await _clienteRepository.ObterPorEmailAsync(email);
        }
    }
}
