using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantOrderingSystem.Models;

public enum UserRole
{
    admin,
    customer
}

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("password")]
    public string Password { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("authProvider")]
    public string AuthProvider { get; set; } = "local";

    [BsonElement("role")]
    [BsonRepresentation(BsonType.String)]
    public UserRole Role { get; set; } = UserRole.customer;
}