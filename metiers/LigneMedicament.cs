using metiers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class LigneMedicament
{
    [Key]
    public int ligneID { get; set; }
    public int qtePrescrite { get; set; }
    public int? qteDelivre { get; set; }
    public DateTime? dateDelivre { get; set; }

    public string dose { get; set; }
    public Statut statut { get; set; } = Statut.EnAttente;
    public int ordID { get; set; }
    public int MedicamentID { get; set; }      
    public virtual Medicament? Medicament { get; set; }


}
