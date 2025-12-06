using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Medecin")]
    public class OrdonnanceController : ControllerBase
    {
        private readonly IOrdonnanceRepository repository;

        public OrdonnanceController(IOrdonnanceRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("medecin/{medecinId}")]
        public async Task<IActionResult> GetOrdonnancesByMedecin(int medecinId)
        {
            var Ordonnances = await repository.GetOrdonnancesByMedecin(medecinId);
            return Ok(Ordonnances);
        }

        [HttpGet("OrdID")]
        public async Task<IActionResult> GetOrdonnanceByID(int id)
        {
            return Ok(await repository.GetOrdonnance(id));

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdonnance(Ordonnance Ordonnance, int medecinId)
        {
            List<Ordonnance> Ordonnances = await repository.GetOrdonnancesByMedecin(medecinId);
            if (Ordonnances.Any(d => d.OrdID == Ordonnance.OrdID))
            {
                return BadRequest("Ordonnance existe!!");
            }
            Ordonnance newOrdonnance = await repository.CreateOrdonnance(Ordonnance);
            return CreatedAtAction(nameof(GetOrdonnanceByID), new { id = newOrdonnance.OrdID }, newOrdonnance);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> updateOrdonnance(int id, Ordonnance Ordonnance)
        {
            var result = await repository.UpdateOrdonnance(Ordonnance);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        [HttpPut("envoyer/{id}")]
        public async Task<IActionResult> EnvoyerOrdonnance(int id, Ordonnance Ordonnance)
        {
            var result = await repository.EnvoyerOrdonnance(Ordonnance);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        [HttpDelete("OrdID")]
        public async Task<IActionResult> deleteOrdonnance(int id)
        {
            var result = await repository.DeleteOrdonnance(id);
            if (result) return NoContent();
            return NotFound("supp impoosible");
        }

        [HttpGet("pharmacien/{id}/dernieres")]
        public async Task<IActionResult> GetDernieresOrdonnancesPharmacien(int id)
        {
            var result = await repository.GetDernieresOrdonnancesPharmacien(id);

            if (result == null || result.Count == 0)
                return NotFound("Aucune ordonnance trouvée.");

            return Ok(result);
        }

        [HttpGet("pharmacien/{pharmacienId}/statistiques/mois/{annee}")]
        public async Task<IActionResult> GetOrdonnancesParMoisPharmacien(int pharmacienId, int annee)
        {
            var result = await repository.GetOrdonnancesParMoisPharmacien(pharmacienId, annee);

            if (result == null || result.Count == 0)
                return NotFound("Aucune ordonnance trouvée pour ce pharmacien.");

            return Ok(result);
        }

    }
}



