using Microsoft.AspNetCore.Identity;
using projet.DTO;
namespace projet.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Adresse { get; set; }
        public string? Specialite { get; set; }
        public string? NomPharmacie { get; set; }
        public Role UserRole { get; set; }

    }
}