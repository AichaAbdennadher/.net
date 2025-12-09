using metiers;
using Microsoft.EntityFrameworkCore;
using projet.Data;

namespace projet.Repositories
{
    public class ligneMedRepository : IligneMedRepository
    {
        private readonly ApplicationContext context;

        public ligneMedRepository(ApplicationContext context)
        {
            this.context = context;
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

        public async Task<bool> DelivrerLigneMedicament(int ligneId)
        {
            var ligne = await context.lignesMedicaments
                                     .Include(l => l.Medicament)
                                     .FirstOrDefaultAsync(l => l.ligneID == ligneId);

            if (ligne == null || ligne.Medicament == null)
                return false;

            int stock = ligne.Medicament.Stock;
            int qteDemandee = ligne.qtePrescrite;
            int qteDejaDelivree = ligne.qteDelivre;

            int qteRestante = qteDemandee - qteDejaDelivree;

            if (qteRestante <= 0)
                return false; // Déjà totalement délivrée

            // Quantité à délivrer selon le stock
            int qteAjustee = Math.Min(stock, qteRestante);

            // Mise à jour de la quantité délivrée
            ligne.qteDelivre += qteAjustee;

            // Mise à jour du stock du médicament
            ligne.Medicament.Stock -= qteAjustee;

            // Mise à jour du statut
            if (ligne.qteDelivre == ligne.qtePrescrite)
                ligne.statut = Statut.Delivree;
            else if (ligne.qteDelivre > 0 && ligne.qteDelivre < ligne.qtePrescrite)
                ligne.statut = Statut.PartiellementDelivree;
            else
                ligne.statut = Statut.EnAttente;

            // Date délivrance
            ligne.dateDelivre = DateTime.Now;

            await context.SaveChangesAsync();
            return true;
        }

    }
}
