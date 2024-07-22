using EF.Constants;
using EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.Contexts
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Asignment> Asignments { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.DefaultConnection);
        }

    }
}
