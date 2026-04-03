using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;

public class RestaurantOrderingDbContext : DbContext
{
    public RestaurantOrderingDbContext (DbContextOptions<RestaurantOrderingDbContext> options) : base(options)
    {   
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
} 