namespace ECommerceWithAuth.MVC.CartService;

public interface ICartService
{
    event Action OnChange;
    Task AddToCart(Product product, int quantity, int maxQuantity);
    Task<List<CartItem>> GetCartItems();
    Task DeleteItem(CartItem item);
    Task EmptyCart();
}

