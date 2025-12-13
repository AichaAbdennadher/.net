using metiers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : IdentityDbContext<projet.Data.ApplicationUser>
{
    public ApplicationContext(DbContextOptions options) : base(options) { }

    public DbSet<projet.Data.ApplicationUser> users { get; set; }
    public DbSet<Medicament> medicaments { get; set; }
    public DbSet<Patient> patients { get; set; }
    public DbSet<Ordonnance> ordonnances { get; set; }
    public DbSet<LigneMedicament> lignesMedicaments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.CIN)
            .IsUnique();

        modelBuilder.Entity<LigneMedicament>()
         .HasOne(l => l.Medicament)
         .WithMany(o => o.LigneMedicaments)
         .HasForeignKey(l => l.MedicamentID)
         .OnDelete(DeleteBehavior.Cascade);





    }

}
