using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Visitor>().ToTable("visitors");
            modelBuilder.Entity<Student>().ToTable("students");
        }
    }
}
