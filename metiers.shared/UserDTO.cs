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
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Tel { get; set; }
        public string Adresse { get; set; }
        public string? Specialite { get; set; }
        public string? NomPharmacie { get; set; }
        public Role Role { get; set; }
    }
}
