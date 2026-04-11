using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;

namespace RestaurantOrderingSystem.Services;

public class CategoryService
{
    private readonly RestaurantOrderingDbContext _context;    

    public CategoryService(RestaurantOrderingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

}