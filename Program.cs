using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RestaurantOrderingSystem.Components;
using RestaurantOrderingSystem.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

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

var connectionString = Environment.GetEnvironmentVariable("MONGO_URI");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");

var mongoClient = new MongoClient(connectionString);

// 1. Database & Auth Services
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddDbContext<RestaurantOrderingDbContext>(options =>
{
    options.UseMongoDB(mongoClient, dbName ?? "CSE325");
});

//EF Core will create a restaurant.db in databasedo
// builder.Services.AddDbContext<RestaurantOrderingDbContext>(options =>
// {
//     options.UseSqlite("Data Source=restaurant.db");
// });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options => options.DetailedErrors = true);

builder.Services.AddSingleton<CartService>();
builder.Services.AddScoped<MenuListService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddSingleton<UIStateService>();

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
