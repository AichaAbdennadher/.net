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

        [StringLength(8, ErrorMessage = "The ID card must contain exactly 8 characters.")]
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

        public Guid MedecinID { get; set; }

    }
}
