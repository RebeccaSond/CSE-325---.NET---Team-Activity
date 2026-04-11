using RestaurantOrderingSystem.Models;
using MongoDB.Driver;

namespace RestaurantOrderingSystem.Services;

public class OrderService
{
    private readonly IMongoCollection<Order> _orders;

    public OrderService(MongoDBService mongoDBService)
    {
        _orders = mongoDBService.Database.GetCollection<Order>("Orders");
    }

    public async Task CreateOrderAsync(string userId, List<MenuItem> items, decimal total)
    {
        var order = new Order
        {
            UserId = userId,
            Items = items,
            Total = total,
            OrderDate = DateTime.UtcNow,
            Status = "Pending"
        };

        await _orders.InsertOneAsync(order);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.UserId, userId);
        return await _orders.Find(filter).ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(string orderId)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
        return await _orders.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateOrderStatusAsync(string orderId, string status)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
        var update = Builders<Order>.Update.Set(o => o.Status, status);
        await _orders.UpdateOneAsync(filter, update);
    }

    public async Task DeleteOrderAsync(string orderId)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
        await _orders.DeleteOneAsync(filter);
    }
}