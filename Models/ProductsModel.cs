using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace RestaurantOrderingSystem.Models

{
    public class Product
    {
        [Column("_id")]
        required public ObjectId Id { get; set; }

        [Column("name")]
        required public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        required public decimal Price { get; set; }

        [Column("categoryId")]
        public ObjectId? CategoryId {get; set;}

        [Column("isAvailable")]
        public bool? isAvailable {get; set;}

        [Column("imageUrl")]
        public string? ImageUrl { get; set; }
    }

}