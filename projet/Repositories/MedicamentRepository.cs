using metiers;
using Microsoft.EntityFrameworkCore;
using projet.Data;

namespace projet.Repositories
{
    public class MedicamentRepository : IMedicamentRepository
    {
        private readonly ApplicationContext context;

        public MedicamentRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Medicament> CreateMedicament(Medicament Medicament)
        {
            if (await context.medicaments.AnyAsync(d => d.Nom.Equals(Medicament.Nom)))
            {
                return null;
            }
            await context.medicaments.AddAsync(Medicament);
            await context.SaveChangesAsync();
            return Medicament;
        }

        public async Task<List<Medicament>> GetMedicamentsPharmacien(int id)
        {
            return await context.medicaments
                               .Where(m => m.UserID == id)   
                               .ToListAsync();
        }

        public async Task<bool> UpdateMedicament(Medicament Medicament)
        {
            var dep = await context.medicaments.FindAsync(Medicament.MedicamentID);
            if (dep == null)
                return false;
            dep.Nom = Medicament.Nom;
            dep.Description = Medicament.Description;
            dep.Stock = Medicament.Stock;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Medicament> GetMedicament(int id)
        {
            return await context.medicaments.FindAsync(id);
        }
    }
}


