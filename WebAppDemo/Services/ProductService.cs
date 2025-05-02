using WebAppDemo.Models;

namespace WebAppDemo.Services;

public class ProductService : IProductService
{
    private readonly ISequenceService _sequenceService;
    private readonly Dictionary<int, Product> _productDico = new Dictionary<int, Product>();

    public ProductService(ISequenceService sequenceService)
    {
        _sequenceService = sequenceService;
        // Initialize with some products
        AddProduct(new Product("Stylo", 3, 120));
        AddProduct(new Product("Crayon", 7, 150));
        AddProduct(new Product("Cahier", 5, 210));
    }

    public int AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        var productId = _sequenceService.NextProductId();
        _productDico.Add(productId, product);
        return productId;
    }

    public bool TryGetProduct(int id, out Product? product)
    {
        return _productDico.TryGetValue(id, out product);
    }

    public Dictionary<int, Product> GetAllProducts()
    {
        return _productDico;
    }

    public async Task<Dictionary<int, Product>> GetAllProductsAsync()
    {
        // Simule une opération asynchrone. Exemple : appel à une base de données
        await Task.Delay(100);
        return GetAllProducts();
    }

    public bool UpdateProduct(int id, Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        if (_productDico.ContainsKey(id))
        {
            _productDico[id] = product;
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateProductAsync(int id, Product product)
    {
        // Simule une opération asynchrone. Exemple : appel à une base de données
        await Task.Delay(100);
        return UpdateProduct(id, product);
    }

    public bool DeleteProduct(int id)
    {
        return _productDico.Remove(id);
    }
}
