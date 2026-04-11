using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models
{
    public class Product
    {
        [Key] // Required for EF Core to track the entity
        [BsonId] // Native driver mapping
        public ObjectId Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }
      
        [Column("price")]
        required public decimal Price { get; set; }
      
        [Column("isAvailable")]
        public bool? isAvailable { get; set; }
      
        [Column("imageUrl")]
        public string? ImageUrl { get; set; }

        [Column("categoryId")]
        public ObjectId? CategoryId { get; set; }
      
        [NotMapped]
        public Category? Category { get; set; }
    }
}