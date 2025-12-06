using metiers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class LigneMedicament
{
    [Key]
    public int ligneID { get; set; }
    public int qtePrescrite { get; set; }
    public int qteDelivre { get; set; }
    public DateTime? dateDelivre { get; set; }

    public string dose { get; set; }

    public Statut statut { get; set; }
    public int ordID { get; set; }

    [ForeignKey("ordID")]
    public virtual Ordonnance Ordonnance { get; set; }

    public int MedicamentID { get; set; }
    [ForeignKey("MedicamentID")]
    public virtual Medicament Medicament { get; set; }
}
