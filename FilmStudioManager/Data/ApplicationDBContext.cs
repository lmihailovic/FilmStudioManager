using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FilmStudioManager.Models;

namespace FilmStudioManager.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FilmEmployee>()
            .HasKey(fe => new { fe.FilmId, fe.EmployeeId });

        modelBuilder.Entity<FilmEmployee>()
            .HasOne<Film>()
            .WithMany(f => f.FilmEmployees)
            .HasForeignKey(fe => fe.FilmId);

        modelBuilder.Entity<FilmEmployee>()
            .HasOne<Employee>()
            .WithMany(e => e.FilmEmployees)
            .HasForeignKey(fe => fe.EmployeeId);
                
        modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, GenreName = "Action" },
            new Genre { GenreId = 2, GenreName = "Drama" },
            new Genre { GenreId = 3, GenreName = "Comedy" },
            new Genre { GenreId = 4, GenreName = "Sci-Fi" },
            new Genre { GenreId = 5, GenreName = "Horror" },
            new Genre { GenreId = 6, GenreName = "Western" }
            );
        }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Employee> Employees { get; set; } 
    public DbSet<FilmEmployee> FilmEmployees { get; set; }
}