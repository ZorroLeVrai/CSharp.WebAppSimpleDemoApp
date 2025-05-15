using Serilog;
using WebAppDemo.Configuration;
using WebAppDemo.Middlewares;
using WebAppDemo.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAppDemo.Swagger;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Lis la config depuis appsettings.json
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Utilisation de Serilog. Remplace le système de log par défaut
builder.Host.UseSerilog();

// Lecture de la section AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Add services to the container.

// Add User Secrets in development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Versioning de l'API
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

    // Choisir la méthode de versioning (ex : URL)
    options.ApiVersionReader = ApiVersionReader.Combine(
           new HeaderApiVersionReader("ApiVersion"),
           new UrlSegmentApiVersionReader()); ; // version dans l’URL
}).AddMvc();

// Access JWT configuration
JwtConfig? jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
if (jwtConfig == null)
{
    throw new Exception("JWT configuration is missing");
}
// Access JWT configuration from user secrets
UserConfig? userConfig = builder.Configuration.GetSection("UserConfig").Get<UserConfig>();
if (userConfig == null)
{
    throw new Exception("User configuration is missing");
}

// Register JWT configuration as a singleton
builder.Services.AddSingleton(jwtConfig);

// Register User configuration as a singleton
builder.Services.AddSingleton(userConfig);

// Add services to the container
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
    };
});

builder.Services.AddAuthorization();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSingleton<IUserService, UserService>();

// Enregistrement du service de configuration
builder.Services.AddSingleton<IMyConfigurationService, MyConfigurationService>();

builder.Services.AddSingleton<ISequenceService, SequenceService>();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Utilisation du logger intégré
app.Logger.LogInformation("Démarrage de l'application");

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

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LogRequestMiddleware>();

app.MapControllers();

app.Run();
