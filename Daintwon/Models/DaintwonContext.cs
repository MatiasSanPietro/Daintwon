using Microsoft.EntityFrameworkCore;

namespace Daintwon.Models
{
    public class DaintwonContext(DbContextOptions<DaintwonContext> options) : DbContext(options)
    {
    }
}
