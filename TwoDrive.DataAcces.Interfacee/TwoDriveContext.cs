using Microsoft.EntityFrameworkCore;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess.Interface
{
    public class TwoDriveContext : DbContext
    {
        public TwoDriveContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
