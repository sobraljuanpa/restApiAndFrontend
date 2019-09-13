using Microsoft.EntityFrameworkCore;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess
{
    public class TwoDriveContext : DbContext
    {
        public TwoDriveContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
