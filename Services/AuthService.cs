using RestaurantOrderingSystem.Models;
using MongoDB.Driver;
using BCrypt.Net;

namespace RestaurantOrderingSystem.Services;

public class AuthService
{
    private readonly MongoDBService _dbService;

    public AuthService(MongoDBService dbService)
    {
        _dbService = dbService;
    }

    public async Task<bool> RegisterAsync(User newUser)
    {
        // 1. Check if user already exists
        var existingUser = await _dbService.Users
            .Find(u => u.Email == newUser.Email)
            .FirstOrDefaultAsync();

        if (existingUser != null) return false;

        // 2. Hash the password
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

        // 3. Save to MongoDB
        await _dbService.Users.InsertOneAsync(newUser);
        return true;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _dbService.Users
            .Find(u => u.Email == email)
            .FirstOrDefaultAsync();

        // Verify the plain text password against the stored hash
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return user;
        }

        return null;
    }
}