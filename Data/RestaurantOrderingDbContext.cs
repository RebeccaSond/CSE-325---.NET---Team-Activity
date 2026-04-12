using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using RestaurantOrderingSystem.Models;

public class RestaurantOrderingDbContext : DbContext
{
    public RestaurantOrderingDbContext(DbContextOptions<RestaurantOrderingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Force EF to look at the exact collection names in MongoDB
        modelBuilder.Entity<Product>().ToCollection("Products");
        modelBuilder.Entity<Category>().ToCollection("Categories");
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
} 
