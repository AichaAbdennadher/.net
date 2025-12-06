using System.ComponentModel.DataAnnotations;
using metiers;

namespace metiers
{
    // Enum pour le rôle : 0 = Pharmacien, 1 = Médecin
    public enum Role
    {
        Pharmacien = 0,
        Medecin = 1
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }

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

        // Champ spécifique au médecin
        public string? Specialite { get; set; }

        // Champ spécifique au pharmacien
        public string? NomPharmacie { get; set; }

        [Required]
        public Role Role { get; set; } // Utilisation de l'enum Role

        public ICollection<Medicament> medicaments { get; set; }

        public virtual ICollection<Patient> patients { get; set; }

         // Ordonnances créées par ce médecin
    public virtual ICollection<Ordonnance> OrdonnancesCreees { get; set; }

    // Ordonnances assignées à ce pharmacien
    //public virtual ICollection<Ordonnance> OrdonnancesAssignées { get; set; }

    }
}
