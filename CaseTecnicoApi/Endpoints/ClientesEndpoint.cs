using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Services.Clientes;
using CaseTecnico.Domain.ResultObject;

namespace CaseTecnicoApi.Endpoints
{
    public class ClientesEndpoint : IEndpoint
    {
        public void DefinirRotas(WebApplication app)
        {
            app.MapGet("api/clientes/", Listar);
            app.MapGet("api/clientes/{email}", ObterPorEmail);
            app.MapPost("api/clientes/", Criar);
            app.MapPut("api/clientes/{id}", Atualizar);
            app.MapDelete("api/clientes/{email}", Excluir);
        }

        async Task<IResult> Listar(IClienteService clienteService)
        {
            var result = await clienteService.Listar();

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.Ok(result.Data);
        }

        async Task<IResult> Criar(IClienteService clienteService, CriarClienteRequest request)
        {
            var result = await clienteService.CriarAsync(request);

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.Created("api/clientes/", result.Data);
        }

        async Task<IResult> Atualizar(IClienteService clienteService, int id, AtualizarClienteRequest request)
        {
            var result = await clienteService.AtualizarAsync(id, request);

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.Ok(result.Data);
        }

        async Task<IResult> Excluir(IClienteService clienteService, string email)
        {
            var result = await clienteService.ExcluirAsync(email);

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.NoContent();
        }

        async Task<IResult> ObterPorEmail(IClienteService clienteService, string email)
        {
            var result = await clienteService.ObterPorEmailAsync(email);

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.Ok(result.Data);
        }
    }
}
