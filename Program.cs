using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Aqui configuramos os servi�os da aplica��o, antes da dar Build(). (ConfigureServices)

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Ignora uma refer�ncia c�clica
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Definindo o provedor de banco de dados
builder.Services.AddDbContext<AppDbContext>(options => 
                    options.UseMySql(mySqlConnection, 
                                     ServerVersion.AutoDetect(mySqlConnection)));

// Finalizando a configura��o de servi�os e executando Build(). (Configure)
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
