using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize] 
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository repository;

        public PatientController(IPatientRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("patients/medecin/{medecinId}")]
        public async Task<IActionResult> GetPatientsByMedecin(Guid medecinId)
        {
            var patients = await repository.GetPatientsByMedecin(medecinId);
            return Ok(patients);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientByID(int id)
        {
            return Ok(await repository.GetPatient(id));

        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient,Guid medecinId)
        {
            List<Patient> patients = await repository.GetPatientsByMedecin(medecinId);
            if (patients.Any(d => d.CIN.Equals(patient.CIN)))
            {
                return BadRequest("patient existe!!");
            }
            Patient newpatient = await repository.CreatePatient(patient);
            return CreatedAtAction(nameof(GetPatientByID), new { id = newpatient.PatientID }, newpatient);
        }

        [HttpPut]
        public async Task<IActionResult> updatePatient(Patient dep)
        {
            var result = await repository.UpdatePatient(dep);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> deletePatient(int id)
        {
            var result = await repository.DeletePatient(id);
            if (result) return NoContent();
            return NotFound("supp impoosible");
        }
    }
}
 

