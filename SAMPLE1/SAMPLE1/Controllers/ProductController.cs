using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAMPLE1.Data;
using SAMPLE1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMPLE1.Controllers

{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await Task.FromResult(_context.Products.ToList());
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
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
            return CreatedAtAction(nameof(GetProducts), new { id = product.Pid }, product);
        }
        [HttpPut]
        public async Task<ActionResult<Product>>UpdateProduct(int id,Product updateProduct)
        {
            if (id != updateProduct.Pid)
            {
                return BadRequest("Product Id mismatched");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();

            }
            existingProduct.PName = updateProduct.PName;
            existingProduct.Price = updateProduct.Price;
            existingProduct.Quantity = updateProduct.Quantity;

            _context.Entry(existingProduct).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>>DeleteProduct(int id)
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
