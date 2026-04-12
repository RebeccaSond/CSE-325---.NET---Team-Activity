using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
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

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    // CREATE
    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    // UPDATE
    public async Task UpdateProductAsync(Product updatedProduct)
    {
        _context.Products.Update(updatedProduct);
        await _context.SaveChangesAsync();
    }

    // DELETE
    public async Task DeleteProductAsync(string id)
    {
        var product = await _context.Products.FindAsync(ObjectId.Parse(id));
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<Product?> GetProductByIdAsync(ObjectId id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetAllProductsWithCategoryAsync()
    {
        var products = await _context.Products.ToListAsync();
        var categories = await _context.Categories.ToListAsync();

        foreach (var product in products)
        {
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
        }

        return products;
    }
}