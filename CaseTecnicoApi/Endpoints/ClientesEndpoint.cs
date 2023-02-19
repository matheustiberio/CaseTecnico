using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Services.Clientes;

namespace CaseTecnicoApi.Endpoints
{
    public class ClientesEndpoint : IEndpoint
    {
        public void DefineRoutes(WebApplication app)
        {
            app.MapGet("api/clientes/", Listar);
            app.MapGet("api/clientes/{email}", ObterPorEmail);
            app.MapPost("api/clientes/", Criar);
            app.MapPut("api/clientes/{id}", Atualizar);
            app.MapDelete("api/clientes/{email}", Excluir);
        }

        async Task<IResult> Listar(IClienteService clienteService)
        {
            var clientes = await clienteService.Listar();

            return Results.Ok(clientes);
        }

        async Task<IResult> Criar(IClienteService clienteService, CriarClienteRequest request)
        {
            var novoCliente = await clienteService.CriarAsync(request);

            if (novoCliente == null)
                return Results.UnprocessableEntity();

            return Results.Ok(novoCliente);
        }

        async Task<IResult> Atualizar(IClienteService clienteService, int id, AtualizarClienteRequest request)
        {
            var clienteAtualizado = await clienteService.AtualizarAsync(id, request);

            if (clienteAtualizado == null)
                return Results.UnprocessableEntity();

            return Results.Ok(clienteAtualizado);
        }

        async Task<IResult> Excluir(IClienteService clienteService, string email)
        {
            bool excluidoComSucesso = await clienteService.ExcluirAsync(email);

            if (!excluidoComSucesso)
                return Results.UnprocessableEntity();

            return Results.NoContent();
        }

        async Task<IResult> ObterPorEmail(IClienteService clienteService, string email)
        {
            var cliente = await clienteService.ObterPorEmailAsync(email);

            if (cliente == null)
                return Results.NotFound();

            return Results.Ok(cliente);
        }
    }
}
