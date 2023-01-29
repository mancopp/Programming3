using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> students { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Student>().ToTable("students");
        }


    }
}
