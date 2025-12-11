using metiers;

namespace projet.Repositories
{
    public interface IMedicamentRepository
    {
        Task<Medicament> CreateMedicament(Medicament Medicament);
        Task<List<Medicament>> GetMedicamentsPharmacien(Guid id); // a partir de token
        Task<Medicament> GetMedicament(int id);
        Task<bool> UpdateMedicament(Medicament Medicament);
        Task<bool> DeleteMedicament(int id); //medic heda wila mamnou3
    }
}
