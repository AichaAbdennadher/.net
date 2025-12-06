using metiers;

namespace projet.DTO
{
    public class OrdonnanceInfoDTO
    {
        public int OrdonnanceID { get; set; }
        public string PatientNom { get; set; }
        public string PatientPrenom { get; set; }
        public string MedecinNom { get; set; }
        public string MedecinPrenom { get; set; }
        public Statut Statut { get; set; }
        public DateTime DateCreation { get; set; }
    }

}
