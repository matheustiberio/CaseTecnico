using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Application.Services.Clientes;
using CaseTecnico.Data.Repositories;
using CaseTecnico.Domain.ResultObject;
using Moq;

namespace CaseTecnico.Application.UnitTests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private ClienteService _sut;

        public ClienteServiceTests()
        {
            _mockClienteRepository = new();
            _sut = new ClienteService(_mockClienteRepository.Object);
        }

        [Fact]
        public async Task CriarCliente_DeveRetornarSucesso_QuandoEmailUnico()
        {
            var request = new CriarClienteRequest
            {
                NomeCompleto = "Teste da Silva",
                Email = "teste@teste.com",
            };

            _mockClienteRepository
                .Setup(x => x.EmailExistente(request.Email, null))
                .ReturnsAsync(false);

            _mockClienteRepository
                .Setup(x => x.CriarAsync(It.IsAny<Cliente>()))
                    .ReturnsAsync(new Cliente(request.NomeCompleto, request.Email));

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.CriarAsync(request);

            Assert.True(result.Success);
            Assert.False(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(request.NomeCompleto, result.Data!.NomeCompleto);
            Assert.Equal(request.Email, result.Data.Email);
        }

        [Fact]
        public async Task CriarCliente_DeveRetornarErro_QuandoEmailNaoUnico()
        {
            var request = new CriarClienteRequest
            {
                NomeCompleto = "Teste da Silva",
                Email = "teste@teste.com",
            };

            _mockClienteRepository
                .Setup(x => x.EmailExistente(request.Email, null))
                .ReturnsAsync(true);

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.CriarAsync(request);

            Assert.False(result.Success);
            Assert.True(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(MensagensErro.EmailJaUtilizado.Codigo, result.Error!.Codigo);
            Assert.Equal(MensagensErro.EmailJaUtilizado.Mensagem, result.Error!.Mensagem);
        }

        [Fact]
        public async Task AtualizarCliente_DeveRetornarSucesso_QuandoClienteExiste()
        {
            int id = 1;
            var request = new AtualizarClienteRequest
            {
                NomeCompleto = "Teste da Silva",
                Email = "teste@teste.com",
            };

            _mockClienteRepository
                 .Setup(x => x.ObterPorIdAsync(id))
                 .ReturnsAsync(new Cliente(request.NomeCompleto, request.Email));

            _mockClienteRepository
                .Setup(x => x.EmailExistente(request.Email, id))
                .ReturnsAsync(false);

            _mockClienteRepository
                .Setup(x => x.AtualizarAsync(It.IsAny<Cliente>()))
                    .ReturnsAsync(new Cliente(request.NomeCompleto, request.Email));

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.AtualizarAsync(id, request);

            Assert.True(result.Success);
            Assert.False(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(request.NomeCompleto, result.Data!.NomeCompleto);
            Assert.Equal(request.Email, result.Data.Email);
        }

        [Fact]
        public async Task AtualizarCliente_DeveRetornarErro_QuandoClienteNaoExiste()
        {
            int id = 1;
            var request = new AtualizarClienteRequest
            {
                NomeCompleto = "Teste da Silva",
                Email = "teste@teste.com",
            };

            _mockClienteRepository
                 .Setup(x => x.ObterPorIdAsync(id))
                 .ReturnsAsync((Cliente)null!);

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.AtualizarAsync(id, request);

            Assert.False(result.Success);
            Assert.True(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(MensagensErro.ClienteNaoEncontrado.Codigo, result.Error!.Codigo);
            Assert.Equal(MensagensErro.ClienteNaoEncontrado.Mensagem, result.Error!.Mensagem);
        }

        [Fact]
        public async Task AtualizarCliente_DeveRetornarErro_QuandoEmailNaoUnico()
        {
            int id = 1;
            var request = new AtualizarClienteRequest
            {
                NomeCompleto = "Teste da Silva",
                Email = "teste@teste.com",
            };

            _mockClienteRepository
                 .Setup(x => x.ObterPorIdAsync(id))
                 .ReturnsAsync(new Cliente(request.NomeCompleto, request.Email));

            _mockClienteRepository
                 .Setup(x => x.EmailExistente(request.Email, id))
                 .ReturnsAsync(true);

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.AtualizarAsync(id, request);

            Assert.False(result.Success);
            Assert.True(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(MensagensErro.EmailJaUtilizado.Codigo, result.Error!.Codigo);
            Assert.Equal(MensagensErro.EmailJaUtilizado.Mensagem, result.Error!.Mensagem);
        }

        [Fact]
        public async Task ObterPorEmailAsync_DeveRetornarSucesso_QuandoClienteExiste()
        {
            string email = "teste@teste.com";

            _mockClienteRepository
                 .Setup(x => x.ObterPorEmailAsync(email))
                 .ReturnsAsync(new Cliente("Teste da Silva", email));

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.ObterPorEmailAsync(email);

            Assert.True(result.Success);
            Assert.False(result.Failure);
            Assert.NotNull(result);
            Assert.IsType<Cliente>(result.Data);
        }

        [Fact]
        public async Task ObterPorEmailAsync_DeveRetornarErro_QuandoClienteNaoExiste()
        {
            string email = "teste@teste.com";

            _mockClienteRepository
                 .Setup(x => x.ObterPorEmailAsync(email))
                 .ReturnsAsync((Cliente)null!);

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.ObterPorEmailAsync(email);

            Assert.False(result.Success);
            Assert.True(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(MensagensErro.ClienteNaoEncontrado.Codigo, result.Error!.Codigo);
            Assert.Equal(MensagensErro.ClienteNaoEncontrado.Mensagem, result.Error!.Mensagem);
        }

        [Fact]
        public async Task Listar_DeveRetornarSucesso_QuandoNaoHouverClientes()
        {
            _mockClienteRepository
                 .Setup(x => x.ListarAsync())
                 .ReturnsAsync(new List<Cliente>());

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.Listar();

            Assert.True(result.Success);
            Assert.False(result.Failure);
            Assert.NotNull(result);
            Assert.IsType<List<Cliente>>(result.Data);
        }

        [Fact]
        public async Task Listar_DeveRetornarSucesso_QuandoHouverClientes()
        {
            _mockClienteRepository
                             .Setup(x => x.ListarAsync())
                             .ReturnsAsync(new List<Cliente>()
                             {
                                 new Cliente("Teste da Silva", "teste@teste.com")
                             });

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.Listar();

            Assert.True(result.Success);
            Assert.False(result.Failure);
            Assert.NotNull(result);
            Assert.IsType<List<Cliente>>(result.Data);
        }

        [Fact]
        public async Task ExcluirCliente_DeveRetornarSucesso_QuandoClienteExiste()
        {
            string email = "teste@teste.com";

            _mockClienteRepository
                .Setup(x => x.ObterPorEmailAsync(email))
                .ReturnsAsync(new Cliente("Teste da Silva", email));

            _mockClienteRepository
                .Setup(x => x.ExcluirAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(true);

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.ExcluirAsync(email);

            Assert.True(result.Data);
            Assert.True(result.Success);
            Assert.False(result.Failure);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExcluirCliente_DeveRetornarErro_QuandoClienteNaoExiste()
        {
            string email = "teste@teste.com";

            _mockClienteRepository
                .Setup(x => x.ObterPorEmailAsync(email))
                .ReturnsAsync((Cliente)null!);

            _sut = new ClienteService(_mockClienteRepository.Object);

            var result = await _sut.ExcluirAsync(email);

            Assert.False(result.Success);
            Assert.True(result.Failure);
            Assert.NotNull(result);
            Assert.Equal(MensagensErro.ClienteNaoEncontrado.Codigo, result.Error!.Codigo);
            Assert.Equal(MensagensErro.ClienteNaoEncontrado.Mensagem, result.Error!.Mensagem);
        }
    }
}