namespace Basket.Api.Entities;
public class ShoppingCart
{
    public string UserName { get; set; } = null!;
    public IEnumerable<ShoppingCartItem> shoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice { 
        get 
        {
            decimal totalPrice = 0;
            foreach (var item in shoppingCartItems)
            {
                totalPrice += item.Price * item.Quantity;
            }
            return totalPrice;
        }
    }

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

}
