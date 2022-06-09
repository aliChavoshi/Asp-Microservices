namespace Basket.API.Entities;

public class ShoppingCart
{
    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();

    public decimal TotalPrice
    {
        get { return Items.Sum(x => x.Price * x.Quantity); }
    }
}