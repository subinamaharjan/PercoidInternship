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
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await Task.FromResult(_context.Customers.ToList());
            return Ok(customers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
           
            var customer = await Task.FromResult(_context.Customers.Find(id));

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Customer>>CreateCustomer(Customer customer)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState); //returns validation errors
            
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, customer);
        }

        [HttpPut]

        public async Task<ActionResult<Customer>>UpdateCustomer(int id, Customer updateCustomer)
        {
            if(id!= updateCustomer.CustomerId)
            {
                return BadRequest("product Id mismatched.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //returns validation errors

            var existingCustomer = await _context.Customers.FindAsync(id);

            var customer = _context.Customers.Find(id);
            if(existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.CustomerName = updateCustomer.CustomerName;
            existingCustomer.CustomerAddress = updateCustomer.CustomerAddress;
            existingCustomer.CustomerPhoneNo = updateCustomer.CustomerPhoneNo;

            _context.Entry(existingCustomer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>>DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if(customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
