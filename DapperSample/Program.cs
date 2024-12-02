using Dapper;
using DapperSample.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        

         static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        string connectionString = "server=DESKTOP-SUBINA;Database=DapperDb;User Id = sa;Password=subina;Encrypt=False;Trusted_Connection=true; ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //create a new product
                var newProduct = new Product
                {
                    Name = "Bottle",
                    Price = 1500,
                    Quantity = 3,
                };

                int newProductId = AddProduct(connection, newProduct);
                Console.WriteLine($"product Added with Id:{newProductId}");

                //get all products
                var products = GetAllProducts(connection);
                Console.WriteLine("\nAll products");
                foreach (var product in products)
                {
                    Console.WriteLine($"Id:{product.Id},Name:{product.Name},Price:{product.Price},Quantity:{product.Quantity}");
                }

                //update a product
                var productToUpdate = products.FirstOrDefault();
                if (productToUpdate != null)
                {
                    productToUpdate.Price = 1400.98M;
                    UpdateProduct(connection, productToUpdate);
                    Console.WriteLine($"\nproduct with Id{productToUpdate.Id} updated.");
                }

                //Delete a Product

                if (products.Count > 0)
                {
                    DeleteProduct(connection, products.Last().Id);
                    Console.WriteLine($"Products with Id {products.Last().Id} deleted.");
                }

            }


            //Add a new Product
            static int AddProduct(SqlConnection connection, Product product)
            {
                string query = "INSERT INTO Products(Name, Price, Quantity) OUTPUT INSERTED.Id VALUES (@Name, @Price, @Quantity)";
                return connection.QuerySingle<int>(query, product);

            }

            //get all products

            static List<Product> GetAllProducts(SqlConnection connection)
            {
                string query = "SELECT *FROM Products";
                return connection.Query<Product>(query).ToList();
            }

            //update product
            static void UpdateProduct(SqlConnection connection,Product product)
            {
                string query = "UPDATE Products SET Name=@Name, Price=@Price, Quantity=@Quantity WHERE Id=@Id";
                connection.Execute(query, product);
            }

            //Delete a product
            static void DeleteProduct(SqlConnection connection,int productId)
            {
                string query = "DELETE FROM Products WHERE Id=@Id";
                connection.Query(query, new { Id = productId });
            }
    }
    }
}