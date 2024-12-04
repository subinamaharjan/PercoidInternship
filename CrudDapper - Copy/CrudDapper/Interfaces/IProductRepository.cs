using CrudDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudDapper.Interfaces
{
   public  interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<Product> GetProductByIdAsync(int Id);
        public Task<int> AddProductAsync(Product product);
        public Task UpdateProductAsync(int id, Product product);
        public Task DeleteProductAsync(int id);
    }
}
