using System;
using System.ComponentModel.DataAnnotations;
using metiers;

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

    }
}
