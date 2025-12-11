using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicamentController : ControllerBase
    {
        private readonly IMedicamentRepository repository;

        public MedicamentController(IMedicamentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("Medicaments/ph/{userId}")]
        public async Task<IActionResult> GetMedicaments(Guid userId)
        {
            var Medicaments = await repository.GetMedicamentsPharmacien(userId);
            return Ok(Medicaments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicament(Medicament Medicament, Guid id)
        {
            List<Medicament> Medicaments = await repository.GetMedicamentsPharmacien( id);
            if (Medicaments.Any(d => d.Nom.Equals(Medicament.Nom)))
            {
                return BadRequest("Medicament existe!!");
            }
            Medicament newMedicament = await repository.CreateMedicament(Medicament);
            return CreatedAtAction(nameof(GetMedicamentByID), new { id = newMedicament.MedicamentID}, newMedicament);
        }

        [HttpPut]
        public async Task<IActionResult> updateMedicament(Medicament Medicament)
        {
            var result = await repository.UpdateMedicament(Medicament);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicamentByID(int id)
        {
            return Ok(await repository.GetMedicament(id));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteMedicament(int id)
        {
            var result = await repository.DeleteMedicament(id);
            if (result) return NoContent();
            return NotFound("supp impoosible");
        }


    }
}




