using Gateway.Api.Aggregators;
using Gateway.Api.Filters;
using Microsoft.ApplicationInsights.TraceListener;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//Ocelot service
var ocelotConfigFile = builder.Environment.IsDevelopment() ? "ocelot.json" : "ocelot.production.json";
builder.Configuration.AddJsonFile(ocelotConfigFile, optional: false, reloadOnChange: true);
builder.Services.AddOcelot()
    .AddSingletonDefinedAggregator<CustomAggregator>()
    .AddSingletonDefinedAggregator<OrderFullAggregator>();

// Add services to the container.
builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ExceptionFilter));
    });

//application insights for azure debugging
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:ConnectionString"]);
Trace.Listeners.Add(new ApplicationInsightsTraceListener());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//console logging  
builder.Logging.AddConsole();

//Ocelot setting
app.UseOcelot().Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
