using metiers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metiers
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }  

        [Required]
        [StringLength(8)]
        public string CIN { get; set; }     

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string Prenom { get; set; }

        [Required]
        public DateTime DateNaissance { get; set; }
        
        [Required]
        [Phone]
        public string Tel { get; set; }

        [Required]
        [StringLength(200)]
        public string Adresse { get; set; }

        public virtual ICollection<Ordonnance> Ordonnances { get; set; }

        //realtion 1-*
        public int MedecinID { get; set; }
        [ForeignKey("UserID")]
        public virtual User medecin { get; set; }

    }
}
