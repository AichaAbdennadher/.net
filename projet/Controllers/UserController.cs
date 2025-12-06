using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projet.Repositories;

namespace projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly IUserRepository repository;

    public UserController(IUserRepository repository)
    {
        this.repository = repository;
    }

        // 🔥 Pharmaciens
        //[Authorize("Medecin")]
        //[HttpGet("pharmaciens")]
        //public async Task<IActionResult> GetPharmaciens()
        //{
        //    var pharmaciens = await repository.GetPharmaciens();

        //    var result = pharmaciens.Select(u => new {
        //        u.UserID,
        //        u.Nom,
        //        u.Prenom,
        //        u.Email,
        //        u.Tel,
        //        u.NomPharmacie
        //    });

        //    return Ok(result);
        //}

        //// 🔥 Médecins sauf connecté
        //[Authorize("Pharmacien")]
        //[HttpGet("medecins")]
        //public async Task<IActionResult> GetMedecinsExceptConnected()
        //{
        //    var idFromToken = int.Parse(User.FindFirst("id").Value);

        //    var medecins = await repository.GetMedecinsExcept(idFromToken);

        //    var result = medecins.Select(u => new {
        //        u.UserID,
        //        u.Nom,
        //        u.Prenom,
        //        u.Email,
        //        u.Tel,
        //        u.Specialite
        //    });

        //    return Ok(result);
        //}


    }
}
