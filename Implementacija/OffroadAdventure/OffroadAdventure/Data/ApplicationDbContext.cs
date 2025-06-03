using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Models;

public class ApplicationDbContext :  IdentityDbContext<User>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Komentar> Komentar { get; set; }
    public DbSet<Notifikacija> Notifikacija { get; set; }
    public DbSet<Placanje> Placanje { get; set; }
    //public DbSet<Role> Role { get; set; }
    public DbSet<StavkaZahtjeva> StavkaZahtjeva { get; set; }
    //public DbSet<UserRole> UserRole { get; set; }
    public DbSet<Vozilo> Vozilo { get; set; }
    public DbSet<ZahtjevZaRentanje> ZahtjevZaRentanje { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Komentar>().ToTable("Komentar");
        modelBuilder.Entity<Notifikacija>().ToTable("Notifikacija");
        modelBuilder.Entity<Placanje>().ToTable("Placanje");
        //modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<StavkaZahtjeva>().ToTable("StavkaZahtjeva");
        //modelBuilder.Entity<UserRole>().ToTable("UserRole");
        modelBuilder.Entity<Vozilo>().ToTable("Vozilo");
        modelBuilder.Entity<ZahtjevZaRentanje>().ToTable("ZahtjevZaRentanje");

        base.OnModelCreating(modelBuilder);
    }
}
