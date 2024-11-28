using Microsoft.EntityFrameworkCore;
using SAMPLE1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMPLE1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }
    }
}
