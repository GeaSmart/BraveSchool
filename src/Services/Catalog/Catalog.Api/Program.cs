using Catalog.Persistence.Database;
using Catalog.Service.Queries;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Common.Logging;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuring DB context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("defaultConnection"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog")
    )
);

//Registering my services
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.Load("Catalog.Service.EventHandlers")));
builder.Services.AddTransient<IProductQueryService, ProductQueryService>();
//Registering health checks services
builder.Services.AddHealthChecks()
    .AddCheck("selfcheck", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<ApplicationDbContext>(typeof(ApplicationDbContext).Name);

// Add Authentication
var secretKey = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("SecretKey")
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //-- We should log in production, but this is for testing purposes
    loggerFactory.AddSyslog(
        builder.Configuration.GetValue<string>("Papertrail:host"),
        builder.Configuration.GetValue<int>("Papertrail:port")
    );
}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

//Configuring health checks
app.MapHealthChecks("/healthcheck", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();