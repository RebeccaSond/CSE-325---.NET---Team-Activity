using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RestaurantOrderingSystem.Components;
using RestaurantOrderingSystem.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// 1. Database & Auth Services
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddScoped<AuthService>();

// 2. Authentication State Management
builder.Services.AddOptions();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "CustomAuth";
}).AddCookie("CustomAuth");

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ProtectedSessionStorage>();


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MongoDb")
    ?? throw new InvalidOperationException("Connection string not found!!");

var mongoClient = new MongoClient(connectionString);

builder.Services.AddDbContext<RestaurantOrderingDbContext>(options =>
{
    options.UseMongoDB(mongoClient, "CSE325");
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options => options.DetailedErrors = true);

builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<MenuListService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
