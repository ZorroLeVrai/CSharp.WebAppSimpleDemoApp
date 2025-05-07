using TaskManagerWebApp.Services;
using System.Text.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Lis la config depuis appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Utilisation de Serilog. Remplace le système de log par défaut
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(item: new JsonStringEnumConverter());
    });


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add custom services
builder.Services.AddSingleton<IIdGeneratorService, IdGeneratorService>();
builder.Services.AddSingleton<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TaskManagerWebApp v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
