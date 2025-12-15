using metiers;
using System.Threading.Tasks;

namespace projet.Repositories
{
    public interface IligneMedRepository
    {
        Task<List<LigneMedicament>> GetLignesMedicamentPharmacien(Guid id);
        Task<List<LigneMedicament>> GetLignesByOrdonnance(int ordID);

        Task<LigneMedicament> GetligneMedicament(int id); //pour pop up de qte delivrée
        Task<LigneMedicament> CreateLigneMedicament(LigneMedicament LigneMedicament);
        Task<bool> UpdateLigneMedicament(LigneMedicament LigneMedicament);
        Task<bool> DeleteLigneMedicament(int id);
        Task<bool> DelivrerLigneMedicament(LigneMedicament ligneMedicament);
    }
  
    }
