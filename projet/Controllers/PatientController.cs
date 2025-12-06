using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Medecin")] 
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository repository;

        public PatientController(IPatientRepository repository)
        {
            this.repository = repository;
        }

   
        [HttpGet("medecin/{medecinId}")]
        public async Task<IActionResult> GetPatientsByMedecin(int medecinId)
        {
            var patients = await repository.GetPatientsByMedecin(medecinId);
            return Ok(patients);
        }

        [HttpGet("PatientID")]
        public async Task<IActionResult> GetPatientByID(int id)
        {
            return Ok(await repository.GetPatient(id));

        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient,int medecinId)
        {
            List<Patient> patients = await repository.GetPatientsByMedecin(medecinId);
            if (patients.Any(d => d.CIN.Equals(patient.CIN)))
            {
                return BadRequest("patient existe!!");
            }
            Patient newpatient = await repository.CreatePatient(patient);
            return CreatedAtAction(nameof(GetPatientByID), new { id = newpatient.PatientID }, newpatient);
        }

        [HttpPut("PatientID")]
        public async Task<IActionResult> updatePatient(int id, Patient patient)
        {
            var result = await repository.UpdatePatient(patient);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        //[HttpDelete("id")]
        //public async Task<IActionResult> deletePatient(int id)
        //{
        //    var result = await repository.DeletePatient(id);
        //    if (result) return NoContent();
        //    return NotFound("supp impoosible");
        //}
    }
}
 

