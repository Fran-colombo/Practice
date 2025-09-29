using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure
{
    public class PracticeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        public PracticeContext(DbContextOptions<PracticeContext> options) : base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
