using RestaurantOrderingSystem.Models;

namespace RestaurantOrderingSystem.Services
{
    // Simple service that keeps track of items added to the cart.
    // Any page can use this to add/remove items.
    public class CartService
    {
        // List of items currently in the cart.
        public List<MenuItem> Items { get; set; } = new();

        // Add a new item to the cart.
        public void Add(MenuItem item)
        {
            Items.Add(item);
        }

        // Remove a specific item from the cart.
        public void Remove(MenuItem item)
        {
            Items.Remove(item);
        }

        // Clear the entire cart.
        public void Clear()
        {
            Items.Clear();
        }

        // Total price of all items in the cart.
        public decimal Total => Items.Sum(i => i.Price);
    }
}