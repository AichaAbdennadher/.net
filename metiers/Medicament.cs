using metiers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metiers
{
    public class Medicament
    {
        [Key]
        public int MedicamentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Le stock ne peut pas être négatif.")]
        public int Stock { get; set; }

        public Guid UserID { get; set; }


    }
}
