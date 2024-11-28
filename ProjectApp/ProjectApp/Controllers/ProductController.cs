using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectApp.DAL;
using ProjectApp.BLL;

namespace ProjectApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var product = _productService.GetAllProducts();
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("{id}")]

        public ActionResult<Product> GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]

        public ActionResult<Product> Post([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);

        }

        [HttpPut("{id}")]

        public ActionResult Put(int id, [FromBody] Product product)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            product.Id = id;
            _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.DeleteProduct(id);
            return NoContent();
        }

        
        
            

            
    }
}
