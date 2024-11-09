using Booky.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Booky.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seeding some categories.
            modelBuilder.Entity<Category>().HasData(
                new Category {Id = 1, Name="Action", DisplayOrder=1},
                new Category {Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category {Id = 3, Name = "History", DisplayOrder = 3 }
             );
        }

    }
}
