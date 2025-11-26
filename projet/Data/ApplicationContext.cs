using metiers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace projet.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>  //: c est extends
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Ordonnance> Ordonances { get; set; }

        public DbSet<LigneMedicament> lignesMedicament { get; set; }



    }


}
