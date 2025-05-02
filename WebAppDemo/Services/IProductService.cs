using WebAppDemo.Models;

namespace WebAppDemo.Services;

public interface IProductService
{
    int AddProduct(Product product);
    bool DeleteProduct(int id);
    Dictionary<int, Product> GetAllProducts();
    Task<Dictionary<int, Product>> GetAllProductsAsync();
    bool TryGetProduct(int id, out Product? product);
    bool UpdateProduct(int id, Product product);
    Task<bool> UpdateProductAsync(int id, Product product);
}