//basic model used by the Menu and the Cart
//Represents one item from the restaurant's menu

namespace RestaurantOrderingSystem.Models
{
    public class MenuItem
    {
        public int Id { get; set; }              // Unique identifier for each menu item
        public string Name { get; set; } = "";   // Name of the dish (what the user will see in the menu)
        public string Description { get; set; } = ""; // Optional description of the dish
        public decimal Price { get; set; }       // Price of the dish
        public string ImageUrl { get; set; } = ""; // Path or URL to the item's image
    }
}