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

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}