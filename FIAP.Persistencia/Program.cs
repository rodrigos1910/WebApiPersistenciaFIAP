using FIAP.Persistencia.Middleware;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Reflection;
using Prometheus;
using MySql.Data.MySqlClient;
using FIAP_Contato.CrossCutting.Logger;
using FIAP_Contato.CrossCutting;
using FIAP_Persistencia.Consumer.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => {
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FIAP - Contato",
        Description = "Gest�o de Contatos",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

//Configura��o para buscar a connection
var connectionString = configuration.GetValue<string>("ConnectionString");
builder.Services.AddScoped<IDbConnection>((connection) => new MySqlConnection(connectionString));

// Configuration IoC
builder.Services.AddRegisterServices();

builder.Logging.ClearProviders();
builder.Logging.AddProvider(
     new CustomLoggerProvider(
        new CustomLoggerProviderConfiguration { LogLevel = LogLevel.Information }
        )
   );

var app = builder.Build();

// Consumers do RabbitMQ
var cadastroConsumer = app.Services.GetRequiredService<CadastroConsumer>();
var atualizacaoConsumer = app.Services.GetRequiredService<AtualizacaoConsumer>(); 
var deleteConsumer = app.Services.GetRequiredService<RemocaoConsumer>();

Task.Run(() => cadastroConsumer.Start());
Task.Run(() => atualizacaoConsumer.Start());
Task.Run(() => deleteConsumer.Start()); // Inicializa o Consumer  

app.UseMetricServer();

//Metricas Prometheus
app.UseHttpMetrics(options =>
{
    options.AddCustomLabel("host", context => context.Request.Host.Host);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseResponseHandleMiddleware();

app.Run();

