using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Order.Persistence.Database;
using Order.Service.Proxies;
using Order.Service.Proxies.Catalog;
using Order.Service.Queries;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
//Configuring DB context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("defaultConnection"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Order")
    )
);

//Registering my services
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.Load("Order.Service.EventHandlers")));
builder.Services.AddTransient<IOrderQueryService, OrderQueryService>();
//API Urls
builder.Services.Configure<ApiUrls>(options => builder.Configuration.GetSection("ApiUrls").Bind(options));
//Proxies

builder.Services.AddHttpClient<ICatalogProxy, CatalogProxy>();

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


builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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

app.Run();
