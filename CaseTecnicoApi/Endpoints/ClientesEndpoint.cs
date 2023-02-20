using CaseTecnico.Application.Contracts.Extensions;
using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Models.Entities;
using CaseTecnico.Application.Services.Clientes;
using CaseTecnico.Domain.ResultObject;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(Summary = "Lista todos os clientes.", Description = "Lista todos os clientes cadastrados na base.")]
        [SwaggerResponse(200, "Ok", typeof(List<Cliente>))]
        async Task<IResult> Listar(IClienteService clienteService)
        {
            var result = await clienteService.Listar();

            return Results.Ok(result.Data);
        }

        [SwaggerOperation(Summary = "Cria um cliente.", Description = "Cria um cliente com o nome e e-mail requisitado. O e-mail deve ser único.")]
        [SwaggerResponse(201, "Created", typeof(Cliente))]
        [SwaggerResponse(400, "BadRequest", typeof(ValidationRequestResult))]
        [SwaggerResponse(422, "UnprocessableEntity", typeof(Error))]
        async Task<IResult> Criar(IClienteService clienteService, IValidator<CriarClienteRequest> validator, CriarClienteRequest request)
        {
            var validationResult = request.ValidateRequest(validator);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var result = await clienteService.CriarAsync(request);

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.Created("api/clientes/", result.Data);
        }
        
        [SwaggerOperation(Summary = "Atualiza um cliente.", Description = "Atualiza um cliente com o nome e e-mail requisitado.")]
        [SwaggerResponse(200, "Ok", typeof(Cliente))]
        [SwaggerResponse(400, "BadRequest", typeof(ValidationRequestResult))]
        [SwaggerResponse(422, "UnprocessableEntity", typeof(Error))]
        async Task<IResult> Atualizar(IClienteService clienteService, IValidator<AtualizarClienteRequest> validator, int id, AtualizarClienteRequest request)
        {
            var validationResult = request.ValidateRequest(validator);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var result = await clienteService.AtualizarAsync(id, request);

            if (result.Failure)
                return Results.UnprocessableEntity(result.Error);

            return Results.Ok(result.Data);
        }

        [SwaggerOperation(Summary = "Exclui um cliente.", Description = "Exclui um cliente que tem o e-mail requisitado.")]
        [SwaggerResponse(204, "NoContent")]
        [SwaggerResponse(404, "NotFound", typeof(Error))]
        async Task<IResult> Excluir(IClienteService clienteService, string email)
        {
            var result = await clienteService.ExcluirAsync(email);

            if (result.Failure)
                return Results.NotFound(result.Error);

            return Results.NoContent();
        }

        [SwaggerOperation(Summary = "Obtem um cliente.", Description = "Obtem um cliente com o e-mail requisitado.")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "NotFound", typeof(Error))]
        async Task<IResult> ObterPorEmail(IClienteService clienteService, string email)
        {
            var result = await clienteService.ObterPorEmailAsync(email);

            if (result.Failure)
                return Results.NotFound(result.Error);

            return Results.Ok(result.Data);
        }
    }
}
