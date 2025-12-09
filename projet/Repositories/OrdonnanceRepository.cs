using metiers;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.DTO;

namespace projet.Repositories
{
    public class OrdonnanceRepository : IOrdonnanceRepository
    {
        private readonly ApplicationContext context;

        public OrdonnanceRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<Ordonnance> CreateOrdonnance(Ordonnance Ordonnance)
        {
            if (await context.ordonnances.AnyAsync(d => d.OrdID == Ordonnance.OrdID))
            {
                return null;
            }
            await context.ordonnances.AddAsync(Ordonnance);
            await context.SaveChangesAsync();
            return Ordonnance;
        }

        public async Task<List<Ordonnance>> GetOrdonnancesByMedecin(Guid medecinId)
        {
            return await context.ordonnances.Where(p => p.PharmacienID == medecinId).ToListAsync();
        }

        public async Task<Ordonnance> GetOrdonnance(int id)
        {
            return await context.ordonnances.FindAsync(id);
        }

        /// /////
        public async Task<bool> UpdateOrdonnance(Ordonnance Ordonnance)
        {
            var dep = await context.ordonnances.FindAsync(Ordonnance.OrdID);
            if (dep == null)
                return false;
            dep.PatientID = Ordonnance.PatientID;
            dep.LigneMedicaments = Ordonnance.LigneMedicaments;
            await context.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteOrdonnance(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EnvoyerOrdonnance(Ordonnance Ordonnance)
        {
            var dep = await context.ordonnances.FindAsync(Ordonnance.OrdID);
            if (dep == null)
                return false;
            dep.envoyee = true;
            await context.SaveChangesAsync();
            return true;

        }

        //Dashboard pharmcien
        public async Task<int> GetNbreOrdonnance(Guid userId)
        {
            return await context.ordonnances
                                .CountAsync(o => o.PharmacienID == userId);
        }

        public async Task<int> GetNbreOrdonnanceLivree(Guid userId)
        {
            return await context.ordonnances
                                .CountAsync(o => o.PharmacienID == userId
                                              && o.Statut == Statut.Delivree);
        }


        public async Task<int> GetNbreOrdonnanceNonLivree(Guid userId)
        {
            return await context.ordonnances
                                .CountAsync(o => o.PharmacienID == userId
                                              && (o.Statut == Statut.EnAttente
                                                  || o.Statut == Statut.PartiellementDelivree));
        }

        public async Task<List<OrdonnanceInfoDTO>> GetDernieresOrdonnancesPharmacien(Guid pharmacienId)
        {
            return await context.ordonnances
                .Include(o => o.Patient)
                .Join(context.users,
                o => o.MedecinID.ToString(),
                u => u.Id,
                (o, u) => new { o, Medecin = u })
                .Where(x => x.o.PharmacienID == pharmacienId)
                .OrderByDescending(x => x.o.DateCreation)
                .Take(5)
                .Select(x => new OrdonnanceInfoDTO
                {
                    OrdonnanceID = x.o.OrdID,
                    PatientNom = x.o.Patient.Nom,
                    PatientPrenom = x.o.Patient.Prenom,
                    MedecinNom = x.Medecin.Nom,
                    MedecinPrenom = x.Medecin.Prenom,
                    Statut = x.o.Statut,
                    DateCreation = x.o.DateCreation
                })
                .ToListAsync();

        }

        public async Task<List<OrdonnanceParMoisDTO>> GetOrdonnancesParMoisPharmacien(Guid pharmacienId, int annee)
        {
            return await context.ordonnances
                .Where(o => o.PharmacienID == pharmacienId && o.DateCreation.Year == annee)
                .GroupBy(o => o.DateCreation.Month)
                .Select(g => new OrdonnanceParMoisDTO
                {
                    Mois = g.Key,
                    Nombre = g.Count()
                })
                .OrderBy(x => x.Mois)
                .ToListAsync();
        }



    }
}
