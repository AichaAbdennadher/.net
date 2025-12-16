using metiers;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using System;
using System.Reflection.Metadata.Ecma335;

namespace projet.Repositories
{
    public class ligneMedRepository : IligneMedRepository
    {
        private readonly ApplicationContext context;

        public ligneMedRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<LigneMedicament> CreateLigneMedicament(LigneMedicament LigneMedicament)
        {
            if (await context.lignesMedicaments.AnyAsync(d => d.ligneID == LigneMedicament.ligneID))
            {
                return null;
            }
            LigneMedicament.Medicament = null;
            await context.lignesMedicaments.AddAsync(LigneMedicament);
            await context.SaveChangesAsync();
            return LigneMedicament;
        }
        public async Task<bool> UpdateLigneMedicament(LigneMedicament ligneMedicament)
        {
            // Récupérer la ligne de médicament dans la table "lignesMedicaments"
            var existing = await context.lignesMedicaments
                .Include(l => l.Medicament) // si tu veux accéder aux infos du médicament
                .FirstOrDefaultAsync(l => l.ligneID == ligneMedicament.ligneID);

            if (existing == null)
                return false;

            if (existing.Medicament == null)
                return false;

            // Calculer la différence de quantité prescrite
            //int diffQte = ligneMedicament.qtePrescrite - existing.qtePrescrite;

            // Vérifier si le stock est suffisant pour la modification
           // if (diffQte > 0 && existing.Medicament.Stock < diffQte)
           //     return false; // Stock insuffisant

            // Mettre à jour le stock du médicament
         //   existing.Medicament.Stock -= diffQte;
            // Mettre à jour les champs
            existing.MedicamentID = ligneMedicament.MedicamentID;
            existing.dose = ligneMedicament.dose;
            existing.qtePrescrite = ligneMedicament.qtePrescrite;
            existing.qteDelivre = ligneMedicament.qteDelivre;
            existing.dateDelivre = ligneMedicament.dateDelivre;
            existing.statut = ligneMedicament.statut;

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DelivrerLigneMedicament(LigneMedicament model)
        {
            var existing = await context.lignesMedicaments
                .Include(l => l.Medicament)
                .FirstOrDefaultAsync(l => l.ligneID == model.ligneID);

            if (existing == null || existing.Medicament == null)
                return false;

            int qteDemandee = model.qteDelivre ?? 0;

            // Sécurités
            if (qteDemandee <= 0)
                return false;

            int dejaDelivre = existing.qteDelivre ?? 0;
            int restante = existing.qtePrescrite - dejaDelivre;

            if (qteDemandee > restante)
                return false;

            if (existing.Medicament.Stock < qteDemandee)
                return false;

            // ✅ Mise à jour correcte
            existing.qteDelivre = dejaDelivre + qteDemandee;
            existing.Medicament.Stock -= qteDemandee;
            existing.dateDelivre = DateTime.Now;

            // ✅ Statut
            if (existing.qteDelivre == existing.qtePrescrite)
                existing.statut = Statut.Delivree;
            else
                existing.statut = Statut.PartiellementDelivree;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLigneMedicament(int id)
        {
            var ligne = await context.lignesMedicaments
                .Include(l => l.Medicament)
                .FirstOrDefaultAsync(l => l.ligneID == id);

            if (ligne == null)
                return false; // ligne non trouvée

            // Restaurer le stock
         //   ligne.Medicament.Stock += ligne.qtePrescrite;

            context.lignesMedicaments.Remove(ligne);
            await context.SaveChangesAsync();

            return true; // suppression réussie
        }


        public async Task<List<LigneMedicament>> GetLignesByOrdonnance(int ordID)
        {
            return await context.lignesMedicaments
                .Where(l => l.ordID == ordID)
                .Select(l => new LigneMedicament
                {
                    ligneID = l.ligneID,
                    ordID = l.ordID,
                    MedicamentID = l.MedicamentID,
                    dose = l.dose,
                    qtePrescrite = l.qtePrescrite,
                    qteDelivre = l.qteDelivre,
                    dateDelivre = l.dateDelivre,
                    statut = l.statut,
                    Medicament = l.Medicament != null ? new Medicament { Nom = l.Medicament.Nom, MedicamentID = l.Medicament.MedicamentID } : null
                })
                .ToListAsync();
        }

        public async Task<List<LigneMedicament>> GetLignesMedicamentPharmacien(Guid pharmacienId)
        {
            return await context.lignesMedicaments
                                .Include(lm => lm.Medicament) // Inclut le médicament pour accéder à son UserID et stock
                                .Where(lm => lm.Medicament.UserID == pharmacienId)
                                .ToListAsync();
        }

        public async Task<LigneMedicament> GetligneMedicament(int id)
        {
            return await context.lignesMedicaments.FindAsync(id);
        }

       

    }
}
