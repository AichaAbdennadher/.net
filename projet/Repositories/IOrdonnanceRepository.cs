using metiers;
using projet.DTO;

namespace projet.Repositories
{
    public interface IOrdonnanceRepository
    {
        Task<Ordonnance> CreateOrdonnance(Ordonnance Ordonnance);
        Task<List<Ordonnance>> GetOrdonnancesByMedecin(Guid medecinId);
        Task<Ordonnance> GetOrdonnance(int id);
     
        Task<bool> DeleteOrdonnance(int id);

        Task<bool> UpdateOrdonnance(Ordonnance Ordonnance); /////

        //envoyer ordanance en cas accpation de Ordonnance (envoyé=true)
        Task<bool> EnvoyerOrdonnance(Ordonnance Ordonnance);
        Task<int> GetNbreOrdonnance(Guid id); //pour chaque pharmacien //id a partir de token 
        Task<int> GetNbreOrdonnanceLivree(Guid id); //pour chaque pharmacien
        Task<int> GetNbreOrdonnanceNonLivree(Guid id); //pour chaque pharmacien
        Task<List<OrdonnanceInfoDTO>> GetDernieresOrdonnancesPharmacien(Guid pharmacienId);
        Task<List<OrdonnanceParMoisDTO>> GetOrdonnancesParMoisPharmacien(Guid pharmacienId, int annee);



    }
}
