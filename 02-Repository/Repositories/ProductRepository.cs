using RepositoryDemo.Models;

namespace RepositoryDemo.Repositories;

public class ProductRepository : IRepository<Product>
{
   private readonly List<Product> _products;

    public ProductRepository()
    {
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1299.99m, StockQuantity = 10 },
            new Product { Id = 2, Name = "Smartphone", Description = "Latest smartphone model", Price = 799.99m, StockQuantity = 15 },
            new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, StockQuantity = 20 }
        };
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await Task.FromResult(_products);

    public async Task<Product> GetByIdAsync(int id) => await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public async Task<Product> AddAsync(Product entity)
    {
        if (entity.Id == 0)
        {
            entity.Id = _products.Max(p => p.Id) + 1;
        }
        
        _products.Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == entity.Id);
        if (existingProduct == null)
        {
            return null;
        }

        int index = _products.IndexOf(existingProduct);
        _products[index] = entity;
        
        return await Task.FromResult(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return await Task.FromResult(false);
        }

        _products.Remove(product);
        return await Task.FromResult(true);
    }
}