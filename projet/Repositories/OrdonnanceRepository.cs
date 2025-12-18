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
            return await context.ordonnances
                .Where(o => o.MedecinID == medecinId)
                .Include(o => o.Patient)
                .ToListAsync();
        }
        public async Task<List<Ordonnance>> GetOrdonnancesEnvoyes(Guid PharmacienID)
        {
            return await context.ordonnances
                .Where(o => o.PharmacienID == PharmacienID && o.envoyee == true)
                .Include(o => o.Patient)
                .ToListAsync();
        }


        public async Task<Ordonnance> GetOrdonnance(int id)
        {
            return await context.ordonnances
                     .Include(o => o.Patient)
                     .FirstOrDefaultAsync(o => o.OrdID == id);

        }


        public async Task<bool> UpdateOrdonnance(Ordonnance Ordonnance)
        {
            var dep = await context.ordonnances.FindAsync(Ordonnance.OrdID);
            if (dep == null)
                return false;
            dep.PatientID = Ordonnance.PatientID;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrdonnance(int id)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // Charger l'ordonnance
                var ordonnance = await context.ordonnances.FindAsync(id);
                if (ordonnance == null)
                    return false;

                // Charger les lignes liées
                var lignes = await context.lignesMedicaments
                    .Include(l => l.Medicament)
                    .Where(l => l.ordID == id)
                    .ToListAsync();

                // Restaurer le stock
                //foreach (var ligne in lignes)
                //{
                //    ligne.Medicament.Stock += ligne.qtePrescrite;
                //}

                // Supprimer les lignes
                context.lignesMedicaments.RemoveRange(lignes);

                // Supprimer l'ordonnance
                context.ordonnances.Remove(ordonnance);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> EnvoyerOrdonnance(Ordonnance ordonnance)
        {
            // Récupération de l'ordonnance depuis le contexte
            var dep = await context.ordonnances.FindAsync(ordonnance.OrdID);
            if (dep == null)
                return false;

            // Modification des propriétés sur l'entité attachée
            dep.envoyee = true;
            dep.Statut = Statut.EnAttente;

            // Sauvegarde dans la base
            await context.SaveChangesAsync();
            return true;
        }


        //Dashboard pharmcien
        public async Task<int> GetNbreOrdonnance(Guid userId)
        {
            return await context.ordonnances
                                .CountAsync(o => o.PharmacienID == userId);
        }

        public async Task<int> GetNbreDoctors(Guid pharmacienId)
        {
            return await context.ordonnances
                .Where(o => o.PharmacienID == pharmacienId)
                .Select(o => o.MedecinID)
                .Distinct()
                .CountAsync();
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

        public async Task<List<Ordonnance>> GetDernieresOrdonnancesPharmacien(Guid pharmacienId)
        {
            return await context.ordonnances
                .Include(o => o.Patient)
                .Where(o => o.PharmacienID == pharmacienId)
                .OrderByDescending(o => o.OrdID)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<Ordonnance>> GetPatients(Guid pharmacienId)
        {
            return await context.ordonnances
                .Include(o => o.Patient)
                .Where(o => o.PharmacienID == pharmacienId)
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
        public async Task<List<LigneMedicament>> DelivrerOrdonnance(int ordId)
        {
            // Récupérer l'ordonnance
            var ordonnance = await context.ordonnances
                .FirstOrDefaultAsync(o => o.OrdID == ordId);

            if (ordonnance == null) return null;

            // Récupérer les lignes de médicaments pour cette ordonnance (séparément)
            var lignes = await context.lignesMedicaments
                .Where(l => l.ordID == ordId)
                .Include(l => l.Medicament) // pour accéder au stock
                .ToListAsync();

            List<LigneMedicament> lignesNonDelivrees = new List<LigneMedicament>();

            foreach (var ligne in lignes)
            {
                int stock = ligne.Medicament?.Stock ?? 0;
                int reste = ligne.qtePrescrite - (ligne.qteDelivre ?? 0);
                if (reste <= 0) continue; // déjà délivrée

                if (reste <= stock)
                {
                    ligne.qteDelivre = ligne.qtePrescrite;
                    ligne.dateDelivre = DateTime.Now;
                    ligne.statut = Statut.Delivree;

                    // Décrémenter le stock
                    ligne.Medicament.Stock -= reste;
                }
                else
                {
                    lignesNonDelivrees.Add(ligne);
                }
            }
            
            if (lignesNonDelivrees.Count == 0)
            {
                ordonnance.Statut = Statut.Delivree;
            }
            else
            {
                ordonnance.Statut = Statut.PartiellementDelivree;
            }

            await context.SaveChangesAsync();

            return lignesNonDelivrees;
        }
    }
}