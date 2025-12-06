using metiers;

namespace projet.Repositories
{
    public interface IligneMedRepository
    {
        Task<List<LigneMedicament>> GetLignesMedicamentPharmacien(int id);

        Task<LigneMedicament> GetligneMedicament(int id); //pour pop up de qte delivrée

        Task<bool> DelivrerLigneMedicament(int ligneId);

    }
  
    }
