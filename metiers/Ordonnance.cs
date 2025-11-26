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

        [Required]
        public Statut Statut { get; set; } = Statut.EnAttente;

        // --- Relation avec Patient ---
        [Required]
        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; } //ok

        // --- Relation 1-*
        [ForeignKey("UserID")]
        public virtual User Medecin { get; set; }

        // --- Lignes de médicaments ---
        public ICollection<LigneMedicament> LigneMedicaments { get; set; }
    }
}
