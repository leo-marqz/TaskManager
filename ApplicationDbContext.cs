using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;

namespace TaskManager
{
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Entities.Step> Steps { get; set; }
        public DbSet<Entities.AttachedFile> AttachedFiles { get; set; }
    }
}
