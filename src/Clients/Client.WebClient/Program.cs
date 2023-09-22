using Client.WebClient.Services;
using Client.WebClient.Services.Config;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Cookie authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

//Registering services
builder.Services.AddSingleton(new ApiGatewayUrl(builder.Configuration.GetValue<string>("ApiGatewayUrl")));
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IOrderService, OrderService>();
builder.Services.AddHttpClient<ICustomerService, CustomerService>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
