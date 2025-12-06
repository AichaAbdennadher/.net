using metiers;
using projet.DTO;

namespace projet.Repositories
{
    public interface IOrdonnanceRepository
    {
        Task<Ordonnance> CreateOrdonnance(Ordonnance Ordonnance);
        Task<List<Ordonnance>> GetOrdonnancesByMedecin(int medecinId);
        Task<Ordonnance> GetOrdonnance(int id);
     
        Task<bool> DeleteOrdonnance(int id);

        Task<bool> UpdateOrdonnance(Ordonnance Ordonnance); /////

        //envoyer ordanance en cas accpation de Ordonnance (envoyé=true)
        Task<bool> EnvoyerOrdonnance(Ordonnance Ordonnance);
        Task<int> GetNbreOrdonnance(int id); //pour chaque pharmacien //id a partir de token 
        Task<int> GetNbreOrdonnanceLivree(int id); //pour chaque pharmacien
        Task<int> GetNbreOrdonnanceNonLivree(int id); //pour chaque pharmacien
        Task<List<OrdonnanceInfoDTO>> GetDernieresOrdonnancesPharmacien(int pharmacienId);
        Task<List<OrdonnanceParMoisDTO>> GetOrdonnancesParMoisPharmacien(int pharmacienId, int annee);



    }
}
