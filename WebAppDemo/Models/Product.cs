namespace WebAppDemo.Models;

public class Product
{
    public string Name { get; }
    public int Quantity { get; }
    public int PriceInCents { get; }

    public Product(string name, int quantity, int priceInCents)
    {
        Name = name;
        Quantity = quantity;
        PriceInCents = priceInCents;
    }
}
