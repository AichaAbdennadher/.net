using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using metiers;

namespace metiers
{
    public class LigneMedicament 
    {
        [Key]
        public int ligneID { get; set; }
        public int qtePrescrite { get; set; }
        public int qteDelivre { get; set; }
       public DateTime dateDelivre { get; set; }

        public string dose { get; set; }
        
        public Statut statut { get; set; }  
        public int ordID { get; set; }

        //realtion 1-*
        [ForeignKey("ordID")]
        public virtual Ordonnance ordonnace { get; set; }

        // realtion 1-*
        [ForeignKey("MedicamentID")]
        public virtual Medicament Medicament { get; set; }
    }
}
