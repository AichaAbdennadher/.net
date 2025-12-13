using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using metiers;

namespace metiers
{
    public class Ordonnance
    {
        [Key]
        public int OrdID { get; set; }

        [Required]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public bool envoyee { get; set; } = false;
        [Required]
        public Statut Statut { get; set; } = Statut.EnAttente;
        [Required]
        public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; } //ok
        [Required]
        public Guid MedecinID { get; set; }
        public Guid PharmacienID { get; set; }

        // --- Lignes de médicaments ---
       // public ICollection<LigneMedicament> LigneMedicaments { get; set; }
    }
}
