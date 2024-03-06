using Microsoft.EntityFrameworkCore;

namespace Daintwon.Models
{
    public class DaintwonContext(DbContextOptions<DaintwonContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
