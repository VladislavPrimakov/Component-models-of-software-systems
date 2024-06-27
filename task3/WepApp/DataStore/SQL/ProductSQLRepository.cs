using Microsoft.EntityFrameworkCore;
using WepApp.DataStore.Interfaces;
using WepApp.Models;

namespace WepApp.DataStore.SQL
{
    public class ProductSQLRepository : IProductRepository
    {
        private readonly ApplicationContext db;

        public ProductSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var prod = db.Products.Find(productId);
            if (prod == null) return;
            db.Products.Remove(prod);
            db.SaveChanges();
        }

        public Product? GetProductById(int productId)
        {
            return db.Products.Find(productId);
        }

        public IEnumerable<Product> GetProducts(DateOnly? expired, int? placementId, bool inStock = true)
        {
            IQueryable<Product> products = db.Products.Include(p => p.Placement);
            if (inStock == true)
                products = products.Where(p => p.Count > 0);
            if (inStock == false)
                products = products.Where(p => p.Count == 0);
            if (expired != null)
                products = products.Where(p => p.ExpirationDate <= expired);
            if (placementId != null && placementId != 0)
                products = products.Where(p=> p.PlacementId == placementId);
            return products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            var prod = db.Products.Find(product.ProductId);
            if (prod == null) return;
            prod.Price = product.Price;
            prod.ExpirationDate = product.ExpirationDate;
            prod.PlacementId = product.PlacementId;
            prod.Price = product.Price;
            prod.Name = product.Name;
            prod.Count = product.Count;
            db.SaveChanges();
        }
    }
}
