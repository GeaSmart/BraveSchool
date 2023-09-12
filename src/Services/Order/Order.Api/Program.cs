using Common.Logging;
using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Proxies;
using Order.Service.Proxies.Catalog;
using Order.Service.Queries;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.MapControllers();

app.Run();
