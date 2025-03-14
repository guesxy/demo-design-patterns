using RepositoryDemo.Models;
using RepositoryDemo.Repositories;

namespace RepositoryDemo;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Repository Pattern Demo\n");
        
        IRepository<Product> productRepository = new ProductRepository();
        
        Console.WriteLine("All Products:");
        var products = await productRepository.GetAllAsync();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: ${product.Price}");
        }
        
        Console.WriteLine("\nFetching product with ID 2:");
        var product2 = await productRepository.GetByIdAsync(2);
        Console.WriteLine($"ID: {product2.Id}, Name: {product2.Name}, Description: {product2.Description}");
        
        Console.WriteLine("\nAdding a new product:");
        var newProduct = new Product
        {
            Name = "Tablet",
            Description = "10-inch tablet with high-resolution display",
            Price = 349.99m,
            StockQuantity = 8
        };
        
        await productRepository.AddAsync(newProduct);
        Console.WriteLine($"Added: ID: {newProduct.Id}, Name: {newProduct.Name}");
        
        Console.WriteLine("\nUpdating product with ID 1:");
        var productToUpdate = await productRepository.GetByIdAsync(1);
        productToUpdate.Price = 1199.99m;
        productToUpdate.StockQuantity = 12;
        await productRepository.UpdateAsync(productToUpdate);
        Console.WriteLine($"Updated: ID: {productToUpdate.Id}, New Price: ${productToUpdate.Price}");
        
        Console.WriteLine("\nDeleting product with ID 3:");
        bool deleted = await productRepository.DeleteAsync(3);
        Console.WriteLine($"Product deleted: {deleted}");
        
        Console.WriteLine("\nAll Products after changes:");
        products = await productRepository.GetAllAsync();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: ${product.Price}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}