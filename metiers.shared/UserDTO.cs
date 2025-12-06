using System.ComponentModel.DataAnnotations;

namespace metiers.shared
{
    public enum Role
    {
        Pharmacien = 0,
        Medecin = 1
    }

    public class UserDTO
    {
        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Tel { get; set; }
        public string Adresse { get; set; }
        public string? Specialite { get; set; }
        public string? NomPharmacie { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
