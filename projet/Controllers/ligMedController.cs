using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Pharmacien")]
    public class ligMedController : ControllerBase
    {
        private readonly IligneMedRepository repository;

        public ligMedController(IligneMedRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetligMeds(Guid id)
        {
            var ligMeds = await repository.GetLignesMedicamentPharmacien(id);
            return Ok(ligMeds);
        }

        [HttpGet("ligneID")]
        public async Task<IActionResult> GetlMedByID(int id)
        {
            return Ok(await repository.GetligneMedicament(id));

        }

        [HttpPut("delivrer/{ligneID}")]
        public async Task<IActionResult> DelivrerLigneMedicament(int ligneId)
        {
            var result = await repository.DelivrerLigneMedicament(ligneId);

            if (!result)
                return BadRequest("Erreur : La ligne n'a pas pu être délivrée.");

            return Ok("La ligne a été délivrée avec succès.");
        }
    }
}
