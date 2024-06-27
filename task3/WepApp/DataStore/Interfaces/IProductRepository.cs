using WepApp.DataStore.SQL;
using WepApp.Models;

namespace WepApp.DataStore.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);
        public void DeleteProduct(int productId);
        public IEnumerable<Product> GetProducts(DateOnly? expired, int? placementId, bool inStock = true);
        public Product? GetProductById(int productId);
        public void UpdateProduct(Product product);
    }
}