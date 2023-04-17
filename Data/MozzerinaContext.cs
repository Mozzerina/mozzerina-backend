using Microsoft.EntityFrameworkCore;
using Mozzerina.Models;

namespace Mozzerina.Data
{
    public class MozzerinaContext : DbContext
    {
        public MozzerinaContext(DbContextOptions<MozzerinaContext> options)
        :base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<MenuType> MenuTypes => Set<MenuType>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Ukrainian_CI_AS");
        }
        
    }
}
