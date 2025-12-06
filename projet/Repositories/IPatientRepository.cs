using metiers;

namespace projet.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> CreatePatient(Patient patient);
        Task<List<Patient>> GetPatientsByMedecin(int medecinId);
        Task<Patient> GetPatient(int id);
        Task<bool> UpdatePatient(Patient Patient);
        //Task<bool> DeletePatient(int id); 

    }
}
