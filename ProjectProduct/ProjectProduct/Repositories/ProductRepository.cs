using Microsoft.EntityFrameworkCore;
using ProjectProduct.Data;
//using ProjectProduct.Interfaces;
using ProjectProduct.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectProduct.Repositories
{
    public class ProductRepository 
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all products
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Retrieve a single product by ID
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        // Create a new product
        public async Task<Product> CreateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Update an existing product
        public async Task<Product> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
                throw new ArgumentNullException(nameof(product), "Product not found");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        // Delete a product by ID
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
