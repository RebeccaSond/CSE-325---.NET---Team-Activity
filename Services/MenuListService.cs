using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;

namespace RestaurantOrderingSystem.Services;

public class MenuListService
{
    private readonly RestaurantOrderingDbContext _context;

    public MenuListService(RestaurantOrderingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

}