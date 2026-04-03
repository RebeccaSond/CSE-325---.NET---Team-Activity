namespace RestaurantOrderingSystem.Services
{
    public class UIStateService
    {
        //updating UI with items in cart
        public event Action? OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}