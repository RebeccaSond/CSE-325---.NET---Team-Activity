using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace RestaurantOrderingSystem.Models
{
    public class Category
    {
        [Column("_id")]
        required public ObjectId Id {get; set;}

        [Column("name")]
        required public string Name {get; set;}

        [Column("slug")]
        public string? Slug {get; set;}

        [Column("displayOrder")]
        required public int DisplayOrder {get; set;}
    }
};