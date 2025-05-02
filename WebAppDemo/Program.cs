using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebAppDemo.Configuration;
using WebAppDemo.Middlewares;
using WebAppDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Lis la config depuis appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Utilisation de Serilog. Remplace le syst�me de log par d�faut
builder.Host.UseSerilog();

// Lecture de la section AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Add services to the container.

// Versioning de l'API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

    // Choisir la m�thode de versioning (ex : URL)
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // version dans l�URL
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Enregistrement du service de configuration
builder.Services.AddSingleton<IMyConfigurationService, MyConfigurationService>();

builder.Services.AddSingleton<ISequenceService, SequenceService>();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Utilisation du logger int�gr�
app.Logger.LogInformation("D�marrage de l'application");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "WebAppDemo v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LogRequestMiddleware>();

app.MapControllers();

app.Run();
