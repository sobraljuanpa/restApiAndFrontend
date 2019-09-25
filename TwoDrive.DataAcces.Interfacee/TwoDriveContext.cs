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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Istrator",
                Username = "admin",
                Password = "admin",
                Email = "admin@admin.admin",
                Role = "Admin"
            }, new User {
                Id = 2,
                FirstName = "First",
                LastName = "TestUser",
                Username = "user1",
                Password = "user1",
                Email = "user1@user1.user1",
                Role = "User"
            }, new User {
                Id = 3,
                FirstName = "Second",
                LastName = "TestUser",
                Username = "user2",
                Password = "user2",
                Email = "user2@user2.user2",
                Role = "User"
            });
        }
    }
}
