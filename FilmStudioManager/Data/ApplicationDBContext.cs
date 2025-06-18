using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FilmStudioManager.Models;

namespace FilmStudioManager.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // Koristite ApplicationUser umesto IdentityUser
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Dodatne konfiguracije ako su potrebne
    }
}