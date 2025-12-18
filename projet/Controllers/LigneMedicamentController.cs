using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LigneMedicamentController : ControllerBase
    {
        private readonly IligneMedRepository repository;

        public LigneMedicamentController(IligneMedRepository repository)
        {
            this.repository = repository;
        }
        [Authorize("medecin")]
        [HttpPost]
        public async Task<IActionResult> CreateLigneMedicament(LigneMedicament ligneMedicament)
        { 
            LigneMedicament newLigneMedicament = await repository.CreateLigneMedicament(ligneMedicament);
            return CreatedAtAction(nameof(GetlMedByID), new { id = newLigneMedicament.ligneID }, newLigneMedicament);
        }
        [Authorize("medecin")]
        [HttpPut]
        public async Task<IActionResult> updateLigneMedicament(LigneMedicament ligneMedicament)
        {
            var result = await repository.UpdateLigneMedicament(ligneMedicament);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }
        [Authorize("Pharmacien")]
        [HttpPut("delivrer")]
        public async Task<IActionResult> DelivrerLigneMedicament(LigneMedicament ligneMedicament)
        {
            var result = await repository.DelivrerLigneMedicament(ligneMedicament);

            if (result == Statut.nonEnvoye)
                return BadRequest(Statut.nonEnvoye);

            return Ok(result);
        }
        [Authorize("medecin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteLigneMedicament(int id)
        {
            var result = await repository.DeleteLigneMedicament(id);
            if (result) return NoContent();
            return NotFound("supp impoosible");
        }
        [Authorize]
        [HttpGet("ph/{userId}")]
        public async Task<IActionResult> GetligMeds(Guid userId)
        {
            try
            {
                var ligMeds = await repository.GetLignesMedicamentPharmacien(userId);
                return Ok(ligMeds);
            }
            catch (Exception ex)
            {
                // Log l'erreur
                Console.WriteLine(ex);
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetlMedByID(int id)
        {
            return Ok(await repository.GetligneMedicament(id));

        }

        [HttpGet("byOrdonnance/{ordID}")]
        public async Task<ActionResult<List<LigneMedicament>>> GetByOrdonnance(int ordID)
        {
            var lignes = await repository.GetLignesByOrdonnance(ordID);
            if (lignes == null || !lignes.Any())
                return NotFound();

            return Ok(lignes);
        }

  
    }
}
