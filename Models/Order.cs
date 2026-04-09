using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantOrderingSystem.Models;

[BsonIgnoreExtraElements]
public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; } = null!;

    [BsonElement("items")]
    public List<MenuItem> Items { get; set; } = new();

    [BsonElement("total")]
    public decimal Total { get; set; }

    [BsonElement("orderDate")]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [BsonElement("status")]
    public string Status { get; set; } = "Pending"; // e.g., Pending, Completed, Cancelled
}