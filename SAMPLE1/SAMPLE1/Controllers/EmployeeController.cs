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
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            var employee = await Task.FromResult(_context.Employees.ToList());
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await Task.FromResult(_context.Employees.Find(id));
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //returns validation errors
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>>UpdateEmployee(int id,Employee updateEmployee)

        {
            if (id !=updateEmployee.Id)
            {
                return BadRequest("Employee Id mismatched");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //returns validation error

            var existingEmployee = await _context.Employees.FindAsync(id);
            //var employee = _context.Employees.Find(id);
           if (existingEmployee == null)
            {
                return NotFound("Employee not found");

            }

            existingEmployee.Name = updateEmployee.Name;
            existingEmployee.Address = updateEmployee.Address;
            existingEmployee.Post = updateEmployee.Post;

            _context.Entry(existingEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee== null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
