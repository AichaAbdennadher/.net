using metiers;
using Microsoft.EntityFrameworkCore;
using projet.Data;

namespace projet.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationContext context;

        public PatientRepository(ApplicationContext context)
        {
            this.context = context;
        }
      
        public async Task<Patient> CreatePatient(Patient patient)
        {
            if (await context.patients.AnyAsync(d => d.CIN.Equals(patient.CIN)))
            {
                return null;
            }
            await context.patients.AddAsync(patient);
            await context.SaveChangesAsync();
            return patient;
        }
        public async Task<List<Patient>> GetPatientsByMedecin(Guid medecinId)
        {
            return await context.patients
                .Where(p => p.MedecinID.Equals( medecinId))
                .ToListAsync();
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await context.patients.FindAsync(id);
        }

        public async Task<bool> UpdatePatient(Patient Patient)
        {
            var dep = await context.patients.FindAsync(Patient.PatientID);
            if (dep == null)
                return false;
            dep.Nom = Patient.Nom;
            dep.Prenom = Patient.Prenom;
            dep.Tel = Patient.Tel;
            dep.Adresse = Patient.Adresse;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePatient(int id)
        {
            var dep = await context.patients.FindAsync(id);
            if (dep == null)
                return false;
            context.patients.Remove(dep);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
