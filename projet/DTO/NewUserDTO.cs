using System.ComponentModel.DataAnnotations;

namespace projet.DTO
{
    public class NewUserDTO
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string CIN { get; set; }
        public string password { get; set; }
        public string tel { get; set; }
        public string adresse { get; set; }

    }
}
