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

        // --- Relation avec Patient ---
        [Required]
        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; } //ok

        // --- Médecin qui crée l'ordonnance ---
        [Required]
        public int MedecinID { get; set; }

        [ForeignKey("MedecinID")]
        public virtual User Medecin { get; set; }  // référence vers User où Role = Medecin

        // --- Pharmacien choisi ---
        [Required]
        public int PharmacienID { get; set; }

        [ForeignKey("PharmacienID")]
        public virtual User Pharmacien { get; set; } // référence vers User où Role = Pharmacien

        //Récupération des ordonnances :
        //Pour accéder au médecin : ordonnance.Medecin.Nom
        //Pour accéder au pharmacien : ordonnance.Pharmacien.Nom

        // --- Lignes de médicaments ---
        public ICollection<LigneMedicament> LigneMedicaments { get; set; }
    }
}
