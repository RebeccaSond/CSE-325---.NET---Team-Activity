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

    public async Task<List<Product>> GetAllProductsWithCategoryAsync()
    {
        try
        {
            // 1. Verify connection by checking if we can even reach the collection
            if (!_context.Database.CanConnect())
            {
                Console.WriteLine("DEBUG: EF Core cannot connect to MongoDB.");
            }

            var products = await _context.Products.ToListAsync();
            Console.WriteLine($"DEBUG: EF Core found {products?.Count ?? 0} products in 'Products' collection.");

            var categories = await _context.Categories.ToListAsync();

            if (products != null)
            {
                foreach (var product in products)
                {
                    product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
                }
            }

            return products ?? new List<Product>();
        }
        catch (Exception ex)
        {
            // This will catch Mapping errors (e.g., if BsonId is not recognized)
            Console.WriteLine($"DB Error in GetAllProducts: {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            return new List<Product>();
        }
    }
}