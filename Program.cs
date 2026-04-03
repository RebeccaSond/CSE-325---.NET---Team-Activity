using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RestaurantOrderingSystem.Components;
using RestaurantOrderingSystem.Services;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("MongoDb")
    ?? throw new InvalidOperationException("Connection string not found!!");

var mongoClient = new MongoClient(connectionString);

builder.Services.AddDbContext<RestaurantOrderingDbContext>(options =>
{
    options.UseMongoDB(mongoClient, "CSE325");
});

//EF Core will create a restaurant.db in databasedo
// builder.Services.AddDbContext<RestaurantOrderingDbContext>(options =>
// {
//     options.UseSqlite("Data Source=restaurant.db");
// });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<CartService>();
builder.Services.AddScoped<MenuListService>();
builder.Services.AddScoped<CategoryService>();
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
