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
    }
}
