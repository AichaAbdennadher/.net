using metiers;
using System.ComponentModel.DataAnnotations;

namespace projet.DTO
{
    public class LigneDelivreDTO
    {
        [Key]
        public int ligneID { get; set; }
        public int qtePrescrite { get; set; }
        public int qteDelivre { get; set; }
        public virtual Medicament Medicament { get; set; }

    }
}
