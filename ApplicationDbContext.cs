using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myproject.Models;

namespace myproject.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyAppUserRegister>
    {
        public DbSet<Book> Books1 { get; set; } // DbSet for the Book entity
        public DbSet<GenreType> GenreTypes1 { get; set; } // DbSet for the GenreType entity

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for the GenreType entity
            modelBuilder.Entity<GenreType>()
                .HasKey(gt => gt.Genre);

            // Configure auto-incrementing column for GenreId in GenreType entity
            modelBuilder.Entity<GenreType>()
                .Property(gt => gt.GenreId);
                //.ValueGeneratedOnAdd(); // This configures GenreId to be auto-incrementing

            // Configure primary key for the Book entity
            modelBuilder.Entity<Book>()
                .HasKey(b => new { b.Title, b.Author });

            // Configure relationship between Book and GenreType
            modelBuilder.Entity<Book>()
                .HasOne<GenreType>() // Assuming GenreType is the navigation property in Book
                .WithMany() // Assuming there's no explicit GenreType entity, just a navigation property
                .HasForeignKey(b => b.Genre); // Assuming Genre is a property in Book
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        //                  .EnableSensitiveDataLogging();
        //}


    }
}





