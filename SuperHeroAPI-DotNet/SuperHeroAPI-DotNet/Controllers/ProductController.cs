using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperHeroAPI_DotNet.Data;
using SuperHeroAPI_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {

            _context = context;
             }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await Task.FromResult(_context.Products.ToList());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)

            {
        var product = await Task.FromResult(_context.Products.Find(id));

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
            }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product UpdateProduct)
        {
            if(id!= UpdateProduct.Id)
            {
                return BadRequest("Id not found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = UpdateProduct.Name;
            existingProduct.Price = UpdateProduct.Price;
            existingProduct.Quantity = UpdateProduct.Quantity;

            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
