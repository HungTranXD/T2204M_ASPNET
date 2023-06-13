using Microsoft.EntityFrameworkCore;
namespace dotNETCoreWebAppMVC.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Product> Products { get; set;}
        
    }
}
