using metiers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //("Medecin")
    public class OrdonnanceController : ControllerBase
    {
        private readonly IOrdonnanceRepository repository;

        public OrdonnanceController(IOrdonnanceRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("medecin/{medecinId}")]
        public async Task<IActionResult> GetOrdonnancesByMedecin(Guid medecinId)
        {
            var Ordonnances = await repository.GetOrdonnancesByMedecin(medecinId);
            return Ok(Ordonnances);
        }
        [HttpGet("pharmacy/{PharmacienID}")]
        public async Task<IActionResult> GetOrdonnancesEnvoyes(Guid PharmacienID)
        {
            var Ordonnances = await repository.GetOrdonnancesEnvoyes(PharmacienID);
            return Ok(Ordonnances);
        }
        [HttpGet("delivree/{ordId}")]
        public async Task<IActionResult> DelivrerOrdonnance(int ordId) { 
            var lignees = await repository.DelivrerOrdonnance(ordId);
            return Ok(lignees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdonnanceByID(int id)
        {
            return Ok(await repository.GetOrdonnance(id));

        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdonnance(Ordonnance Ordonnance, Guid medecinId)
        {
            List<Ordonnance> Ordonnances = await repository.GetOrdonnancesByMedecin(medecinId);
            if (Ordonnances.Any(d => d.OrdID == Ordonnance.OrdID))
            {
                return BadRequest("Ordonnance existe!!");
            }
            Ordonnance newOrdonnance = await repository.CreateOrdonnance(Ordonnance);
            return CreatedAtAction(nameof(GetOrdonnanceByID), new { id = newOrdonnance.OrdID }, newOrdonnance);
        }

        [HttpPut("update")]
        public async Task<IActionResult> updateOrdonnance( Ordonnance Ordonnance)
        {
            var result = await repository.UpdateOrdonnance(Ordonnance);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        [HttpPut("envoyer")]
        public async Task<IActionResult> EnvoyerOrdonnance(Ordonnance Ordonnance)
        {
            var result = await repository.EnvoyerOrdonnance(Ordonnance);
            if (result) return NoContent();
            return BadRequest("erreur update");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteOrdonnance(int id)
        {
            var result = await repository.DeleteOrdonnance(id);
            if (result) return NoContent();
            return NotFound("supp impoosible");
        }


        [HttpGet("pharmacy/nbreOrd/{PharmacienID}")]
        public async Task<IActionResult> GetNbreOrdonnance(Guid PharmacienID)
        {
            var nbre = await repository.GetNbreOrdonnance(PharmacienID);
            return Ok(nbre);
        }
        [HttpGet("pharmacy/nbreOrdLivree/{PharmacienID}")]
        public async Task<IActionResult> GetNbreOrdonnanceLivree(Guid PharmacienID)
        {
            var nbre = await repository.GetNbreOrdonnanceLivree(PharmacienID);
            return Ok(nbre);
        }
        [HttpGet("pharmacy/nbreOrdNonLivree/{PharmacienID}")]
        public async Task<IActionResult> GetNbreOrdonnanceNonLivree(Guid PharmacienID)
        {
            var nbre = await repository.GetNbreOrdonnanceNonLivree(PharmacienID);
            return Ok(nbre);
        }

        [HttpGet("pharmacy/nbreDoctors/{PharmacienID}")]
        public async Task<IActionResult> GetNbreDoctors(Guid PharmacienID)
        {
            var nbre = await repository.GetNbreDoctors(PharmacienID);
            return Ok(nbre);
        }

        [HttpGet("pharmacien/dernieres/{id}")]
        public async Task<IActionResult> GetDernieresOrdonnancesPharmacien(Guid id)
        {
            var result = await repository.GetDernieresOrdonnancesPharmacien(id);

            if (result == null || result.Count == 0)
                return NotFound("Aucune ordonnance trouvée.");

            return Ok(result);
        }

        [HttpGet("pharmacien/{pharmacienId}/statistiques/mois/{annee}")]
        public async Task<IActionResult> GetOrdonnancesParMoisPharmacien(Guid pharmacienId, int annee)
        {
            var result = await repository.GetOrdonnancesParMoisPharmacien(pharmacienId, annee);

            if (result == null || result.Count == 0)
                return NotFound("Aucune ordonnance trouvée pour ce pharmacien.");

            return Ok(result);
        }

    }
}



