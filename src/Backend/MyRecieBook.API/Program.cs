using MyRecieBook.API.Filters;
using MyRecieBook.API.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(); // Nativo do .NET 9
builder.Services.AddSwaggerGen(); // Pacote Swashbuckle

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Ativa o Scalar (Acessível em /scalar/v1)
    app.MapOpenApi();
    app.MapScalarApiReference();

    // Ativa o Swagger (Acessível em /swagger)
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
