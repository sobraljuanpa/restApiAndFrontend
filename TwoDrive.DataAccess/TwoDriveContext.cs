using Microsoft.EntityFrameworkCore;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess
{
    public class TwoDriveContext : DbContext
    {
        public TwoDriveContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
    }
}
