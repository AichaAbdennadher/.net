using metiers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projet.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions options) : base(options) { }

    public DbSet<Medicament> medicaments { get; set; }
    public DbSet<Patient> patients { get; set; }
    public DbSet<Ordonnance> ordonnances { get; set; }
    public DbSet<LigneMedicament> lignesMedicaments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ordonnance → Medecin / Pharmacien : NO ACTION
        modelBuilder.Entity<Ordonnance>()
            .HasOne(o => o.Medecin)
            .WithMany(u => u.OrdonnancesCreees)
            .HasForeignKey(o => o.MedecinID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Ordonnance>()
            .HasOne(o => o.Pharmacien)
            .WithMany()
            .HasForeignKey(o => o.PharmacienID)
            .OnDelete(DeleteBehavior.NoAction);

        // Ordonnance → Patient : NO ACTION
        modelBuilder.Entity<Ordonnance>()
            .HasOne(o => o.Patient)
            .WithMany(p => p.Ordonnances)
            .HasForeignKey(o => o.PatientID)
            .OnDelete(DeleteBehavior.NoAction);

        // LigneMedicament → Ordonnance : CASCADE
        modelBuilder.Entity<LigneMedicament>()
            .HasOne(l => l.Ordonnance)
            .WithMany(o => o.LigneMedicaments)
            .HasForeignKey(l => l.ordID)
            .OnDelete(DeleteBehavior.Cascade);

        // LigneMedicament → Medicament : NO ACTION
        modelBuilder.Entity<LigneMedicament>()
            .HasOne(l => l.Medicament)
            .WithMany(m => m.LignesMedicaments)
            .HasForeignKey(l => l.MedicamentID)
            .OnDelete(DeleteBehavior.NoAction);
    }

}
