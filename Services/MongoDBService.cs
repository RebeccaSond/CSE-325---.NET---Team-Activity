using MongoDB.Driver;
using RestaurantOrderingSystem.Models;

namespace RestaurantOrderingSystem.Services;

public class MongoDBService
{
    private readonly IMongoDatabase _database;

    public MongoDBService()
    {
        // Load .env variables
        DotNetEnv.Env.Load();
        
        var uri = Environment.GetEnvironmentVariable("MONGO_URI");
        if (string.IsNullOrEmpty(uri))
        {
            throw new Exception("CRITICAL: MONGO_URI is missing from .env file!");
        }

        var client = new MongoClient(uri);
        _database = client.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME"));
    }

    public IMongoDatabase Database => _database;

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");

    internal IMongoCollection<T>? GetCollection<T>(string v)
    {
        throw new NotImplementedException();
    }
}