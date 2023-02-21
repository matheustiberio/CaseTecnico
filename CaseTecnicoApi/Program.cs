using CaseTecnicoApi.Extensions;
using CaseTecnicoApi.Middlewares;

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
builder.Services.AddValidators();
builder.Services.AddMiddlewares();

var app = builder.Build();

app.UseEndpoints();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();