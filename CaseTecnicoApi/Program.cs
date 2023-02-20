using CaseTecnico.Application.Contracts.Validators;
using CaseTecnicoApi.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Case Tecnico - PBTech" });
    s.EnableAnnotations();
});

builder.Services.AddServices();
builder.Services.AddEndpoints();
builder.Services.AddDatabaseContexts();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteRequestValidator>();

var app = builder.Build();

app.UseEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();