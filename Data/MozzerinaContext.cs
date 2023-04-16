using Microsoft.EntityFrameworkCore;
using Mozzerina.Models;

namespace Mozzerina.Data
{
    public class MozzerinaContext : DbContext
    {
        public MozzerinaContext(DbContextOptions<MozzerinaContext> options)
        :base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Ukrainian_CI_AS");
        }
        public DbSet<User>? Users => Set<User>();
        public DbSet<Drink>? Drinks => Set<Drink>();
        public DbSet<Food>? Foods => Set<Food>();
        public DbSet<AtHomeCoffee>? AtHomeCoffees => Set<AtHomeCoffee>();
        public DbSet<Merchandise>? Merchandises => Set<Merchandise>();
        public DbSet<GiftCard>? GiftCards => Set<GiftCard>();
    }
}
