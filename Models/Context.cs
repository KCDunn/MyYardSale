using Microsoft.EntityFrameworkCore;

namespace eCommerce.Models
{
    public class StoreContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }

    }
}