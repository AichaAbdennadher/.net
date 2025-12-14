using metiers;
using Microsoft.EntityFrameworkCore;
using projet.Data;
using System;

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
            int diffQte = ligneMedicament.qtePrescrite - existing.qtePrescrite;

            // Vérifier si le stock est suffisant pour la modification
            if (diffQte > 0 && existing.Medicament.Stock < diffQte)
                return false; // Stock insuffisant

            // Mettre à jour le stock du médicament
            existing.Medicament.Stock -= diffQte;
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
                               // .Include(lm => lm.Medicament) // Inclut le médicament pour accéder à son UserID et stock
                               // .Where(lm => lm.Medicament.UserID == pharmacienId)
                                .ToListAsync();
        }

        public async Task<LigneMedicament> GetligneMedicament(int id)
        {
            return await context.lignesMedicaments.FindAsync(id);
        }

        //public async Task<bool> DelivrerLigneMedicament(int ligneId)
        //{
        //    var ligne = await context.lignesMedicaments
        //                            .Include(l => l.Medicament)
        //                             .FirstOrDefaultAsync(l => l.ligneID == ligneId);

        //    if (ligne == null || ligne.Medicament == null)
        //        return false;

        //    int stock = ligne.Medicament.Stock;
        //    int qteDemandee = ligne.qtePrescrite;
        //    int qteDejaDelivree = ligne.qteDelivre;

        //    int qteRestante = qteDemandee - qteDejaDelivree;

        //    if (qteRestante <= 0)
        //        return false; // Déjà totalement délivrée

        //    // Quantité à délivrer selon le stock
        //    int qteAjustee = Math.Min(stock, qteRestante);

        //    // Mise à jour de la quantité délivrée
        //    ligne.qteDelivre += qteAjustee;

        //    // Mise à jour du stock du médicament
        //    ligne.Medicament.Stock -= qteAjustee;

        //    // Mise à jour du statut
        //    if (ligne.qteDelivre == ligne.qtePrescrite)
        //        ligne.statut = Statut.Delivree;
        //    else if (ligne.qteDelivre > 0 && ligne.qteDelivre < ligne.qtePrescrite)
        //        ligne.statut = Statut.PartiellementDelivree;
        //    else
        //        ligne.statut = Statut.EnAttente;

        //    // Date délivrance
        //    ligne.dateDelivre = DateTime.Now;

        //    await context.SaveChangesAsync();
        //    return true;
        //}

    }
}
