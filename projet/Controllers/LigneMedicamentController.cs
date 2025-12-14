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

        [HttpPost]
        public async Task<IActionResult> CreateLigneMedicament(LigneMedicament ligneMedicament)
        {
            //List<LigneMedicament> LigneMedicaments = await repository.GetLignesBy(LigneMedicament.Medicament.Nom);
            //if (LigneMedicaments.Any(d => d.Medicament.Nom == LigneMedicament.Medicament.Nom))
            //{
            //    return BadRequest("LigneMedicament existe!!");
            //}
            LigneMedicament newLigneMedicament = await repository.CreateLigneMedicament(ligneMedicament);
            return CreatedAtAction(nameof(GetlMedByID), new { id = newLigneMedicament.ligneID }, newLigneMedicament);
        }
        [HttpPut]
        public async Task<IActionResult> updateLigneMedicament(LigneMedicament ligneMedicament)
        {
            var result = await repository.UpdateLigneMedicament(ligneMedicament);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }


        [HttpGet]
        public async Task<IActionResult> GetligMeds(Guid id)
        {
            var ligMeds = await repository.GetLignesMedicamentPharmacien(id);
            return Ok(ligMeds);
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

        //[HttpPut("delivrer/{ligneId}")]
        //public async Task<IActionResult> DelivrerLigneMedicament(int ligneId)
        //{
        //    var result = await repository.DelivrerLigneMedicament(ligneId);

        //    if (!result)
        //        return BadRequest("Erreur : La ligne n'a pas pu être délivrée.");

        //    return Ok("La ligne a été délivrée avec succès.");
        //}
    }
}
