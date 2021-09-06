using Alinta.Demo.Infrastructure.Schema;
using Microsoft.EntityFrameworkCore;

namespace Alinta.Demo.Infrastructure.Database
{
    public class LocalContext : DbContext
    {
        public LocalContext(DbContextOptions<LocalContext> options)
            : base(options)
        { }
        
        public DbSet<Customer> Customers { get; set; } = null!;
    }
}