using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options) { }

        public DbSet<Order>? Orders { get; set; }
        public DbSet<Employee>? Employee { get; set; }
        public DbSet<Supplier>? Suppliers
        { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Shipper>? Shippers { get; set; }
        public DbSet<Customer>? Customers { get; set; }
    }
}