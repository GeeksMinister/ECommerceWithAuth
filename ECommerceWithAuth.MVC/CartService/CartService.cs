namespace ECommerceWithAuth.MVC.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IToastService _toastService;
    

    public event Action? OnChange;

    public CartService(ILocalStorageService localStorage, IToastService toastService)
    {
        _localStorage = localStorage;
        _toastService = toastService;
    }

    public async Task AddToCart(Product product, int quantity, int maxQuantity)
    {
        var item = new CartItem();
        item.Quantity = quantity;
        item.ProductId = product.Guid;
        item.ProductName = product.Name;
        item.MaxQuantity = maxQuantity;
        item.Price = product.GetCurrentPrice();

        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cart == null)
        {
            cart = new List<CartItem>();
        }

        var sameItem = cart.Find(x => x.ProductId == item.ProductId);
        if (sameItem == null)
        {
            cart.Add(item);
        }
        else
        {
            sameItem.Quantity += item.Quantity;
        }

        await _localStorage.SetItemAsync("cart", cart);

        _toastService.ShowSuccess($"{quantity} {product.Name} | {product.Category!.Name}"
            , "Added to cart:");

        OnChange?.Invoke();
    }

    public async Task<List<CartItem>> GetCartItems()
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cart == null)
        {
            return new List<CartItem>();
        }
        return cart;
    }

    public async Task DeleteItem(CartItem item)
    {
        var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
        if (cart == null)
        {
            return;
        }

        var cartItem = cart.Find(x => x.ProductId == item.ProductId);
        if (cartItem is not null) cart.Remove(cartItem);

        await _localStorage.SetItemAsync("cart", cart);
        OnChange?.Invoke();
    }

    public async Task EmptyCart()
    {
        await _localStorage.RemoveItemAsync("cart");
        OnChange?.Invoke();
    }
}
