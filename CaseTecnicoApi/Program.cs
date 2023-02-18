using CaseTecnico.Application.Contracts.Requests;
using CaseTecnico.Application.Services.Clientes;
using CaseTecnico.Data.Database;
using CaseTecnico.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("ClientesDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/clientes/", async (IClienteService clienteService) =>
{
    var clientes = await clienteService.Obter();

    return Results.Ok(clientes);
});

app.MapPost("/clientes/", async (IClienteService clienteService, CriarClienteRequest request) =>
{
    var novoCliente = await clienteService.CriarAsync(request);

    if (novoCliente == null)
        return Results.UnprocessableEntity();

    return Results.Ok(novoCliente);
});


app.MapPut("/clientes/{id}", async (IClienteService clienteService, int id, AtualizarClienteRequest request) =>
{
    var clienteAtualizado = await clienteService.AtualizarAsync(id, request);

    if (clienteAtualizado == null)
        return Results.UnprocessableEntity();

    return Results.Ok(clienteAtualizado);
});

app.MapDelete("/clientes/{email}", async (IClienteService clienteService, string email) =>
{
    bool excluidoComSucesso = await clienteService.ExcluirAsync(email);

    if (!excluidoComSucesso)
        return Results.UnprocessableEntity();

    return Results.NoContent();
});

app.MapGet("/clientes/{email}", async (IClienteService clienteService, string email) =>
{
    var cliente = await clienteService.ObterPorEmailAsync(email);

    if (cliente == null)
        return Results.NotFound();

    return Results.Ok(cliente);
});

app.Run();