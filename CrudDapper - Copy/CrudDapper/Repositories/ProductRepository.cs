using CrudDapper.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CrudDapper.Models;
using Dapper;


namespace CrudDapper.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public ProductRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(connectionString);
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            const string query = " SELECT Id, Name, Price, Quantity, Colour FROM Products";
            return await _connection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            const string query = "SELECT Id, Name, Price, Quantity, Colour FROM Products WHERE Id=@Id";
            return await _connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<int> AddProductAsync(Product product)
        {
            const string query = @"
                    INSERT INTO Products (Name, Price, Quantity, Colour)
                    OUTPUT INSERTED.Id
                    Values(@Name, @Price, @Quantity, @Colour)";
            return await _connection.QuerySingleAsync<int>(query, product);
        }

        public async Task UpdateProductAsync(int id,Product product)
        {
            const string query = @"
                UPDATE Products
                SET Name=@Name, Price=@Price, Quantity=@Quantity, Colour=@Colour
                WHERE Id=@Id";
            await _connection.ExecuteAsync(query, product);

        }

        public async Task DeleteProductAsync(int id)
        {
            const string query = "DELETE FROM Products WHERE Id=@Id";
            await _connection.ExecuteAsync(query, new { Id = id });
        }

        
    }
}
