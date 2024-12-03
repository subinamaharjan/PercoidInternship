using CrudDapper.Interfaces;
using CrudDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapper.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetProductsAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _repository.GetProductByIdAsync(id);
        }
        public async Task<int> AddProductAsync(Product product)
        {
            if(product.Price <= 0)
            {
                throw new ArgumentException("price must be greater than zero");
            }
            return await _repository.AddProductAsync(product);
        }
        public async Task UpdateProductAsync(int id,Product product)
        {
            var existingProduct = await _repository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            if(product.Price <= 0)
            {
                throw new ArgumentException("price must be greater than zero");
            }
            await _repository.UpdateProductAsync(id,product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("product not found.");
            }
            await _repository.DeleteProductAsync(id);
        }

    }
}
