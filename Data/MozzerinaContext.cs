using Microsoft.EntityFrameworkCore;
using Mozzerina.Models;

namespace Mozzerina.Data
{
    public class MozzerinaContext : DbContext
    {
        public MozzerinaContext(DbContextOptions<MozzerinaContext> options)
        :base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
