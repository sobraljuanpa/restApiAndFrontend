using Microsoft.EntityFrameworkCore;
using TwoDrive.Domain;

namespace TwoDrive.DataAccess.Interface
{
    public class TwoDriveContext : DbContext
    {
        public TwoDriveContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<File> Files { get; set; }
    }
}
