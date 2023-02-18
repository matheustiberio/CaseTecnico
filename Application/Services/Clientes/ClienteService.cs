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

        public async Task<Cliente?> AtualizarAsync(int id, AtualizarClienteRequest request)
        {
            //validar existencia de email
            var cliente = await _clienteRepository.ObterPorIdAsync(id);

            if (cliente == null)
                return null;

            //! validar existencia de email

            cliente.NomeCompleto = request.NomeCompleto;
            cliente.Email = request.Email;


            return await _clienteRepository.AtualizarAsync(cliente);
        }

        public async Task<Cliente> CriarAsync(CriarClienteRequest request)
        {
            //! validar existencia de email

            var novoCliente = new Cliente(request.NomeCompleto, request.Email);

            return await _clienteRepository.CriarAsync(novoCliente);
        }

        public async Task<bool> ExcluirAsync(string email)
        {
            var cliente = await _clienteRepository.ObterPorEmailAsync(email);

            if (cliente == null)
                return false;

            return await _clienteRepository.ExcluirAsync(cliente);
        }

        public async Task<List<Cliente>> Obter()
        {
            return await _clienteRepository.ObterAsync();
        }

        public async Task<Cliente?> ObterPorEmailAsync(string email)
        {
            return await _clienteRepository.ObterPorEmailAsync(email);
        }
    }
}
