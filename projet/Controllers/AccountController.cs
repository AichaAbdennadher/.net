using metiers;
using metiers.shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using projet.Data;
using projet.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration configuration;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.roleManager = roleManager;
    }

    // ------------------------ REGISTER ------------------------
    [HttpPost("Register")]
    public async Task<IActionResult> Register(NewUserDTO newUserDTO)
    {
        if (await userManager.FindByEmailAsync(newUserDTO.Email) != null)
            return BadRequest("User existe déjà");

        // Convertir enum → string pour IdentityRole
        var roleStr = newUserDTO.Role.ToString();

        // Créer le rôle s'il n'existe pas
        if (!await roleManager.RoleExistsAsync(roleStr))
            await roleManager.CreateAsync(new IdentityRole(roleStr));

        // Créer l'utilisateur
        var appUser = new ApplicationUser
        {
            UserName = newUserDTO.Email,
            Email = newUserDTO.Email,
            Nom = newUserDTO.Nom,
            Prenom = newUserDTO.Prenom,
            Tel = newUserDTO.Tel,
            Adresse = newUserDTO.Adresse,
            Specialite = newUserDTO.Specialite,
            NomPharmacie = newUserDTO.NomPharmacie,
            UserRole = newUserDTO.Role

        };

        var result = await userManager.CreateAsync(appUser, newUserDTO.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await userManager.AddToRoleAsync(appUser, roleStr);

        return Ok("Utilisateur créé avec succès !");
    }

    // ------------------------ LOGIN ------------------------
    [HttpPost("Login")]
    public async Task<IActionResult> Login(projet.DTO.LoginDTO loginDTO)
    {
        var user = await userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null)
            return Unauthorized("Invalid credentials");

        if (!await userManager.CheckPasswordAsync(user, loginDTO.Password))
            return Unauthorized("Invalid credentials");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("UserRole", user.UserRole.ToString())
        };

        var roles = await userManager.GetRolesAsync(user);

        foreach (var r in roles)
            claims.Add(new Claim(ClaimTypes.Role, r));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds
        );
        var finalToken = new JwtSecurityTokenHandler().WriteToken(token);
        var result = new
        {
            token = finalToken,
            email = user.Email,
            id = user.Id,
            role = user.UserRole.GetDisplayName()
        };
        return Ok(result);
    }

    // ------------------------ GET CURRENT USER ------------------------
    [Authorize]
    [HttpGet("Me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return NotFound();
        return Ok(new UserDTO()
        {
           Email = user.Email,
           Nom = user.Nom,
           Adresse = user.Adresse,
           NomPharmacie = user.NomPharmacie,
           Prenom = user.Prenom,
           Specialite = user.Specialite,
           Tel = user.Tel,

        });
    }
    [Authorize]
    [HttpGet("Doctor/{medecinId:guid}")]
    public async Task<IActionResult> GetDoctor(Guid medecinId)
    {
        var user = await userManager.FindByIdAsync(medecinId.ToString());

        if (user == null)
            return NotFound("Médecin introuvable");

        var doctorDto = new UserDTO
        {
            Email = user.Email,
            Nom = user.Nom,
            Prenom = user.Prenom,
            Adresse = user.Adresse,
            Specialite = user.Specialite,
            Tel = user.Tel
        };

        return Ok(doctorDto);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserDTO dto)
    {
        // récupérer l'utilisateur connecté
        var user = await userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        // mise à jour des champs
        user.Nom = dto.Nom;
        user.Prenom = dto.Prenom;
        user.Email = dto.Email;
        user.UserName = dto.Email;
        user.Tel = dto.Tel;
        user.Adresse = dto.Adresse;
        user.NomPharmacie = dto.NomPharmacie;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }

}
