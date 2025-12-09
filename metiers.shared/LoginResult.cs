using System;
using System.Collections.Generic;
using System.Text;

namespace metiers.shared
{
    public class LoginResult
    {
        public string token { get; set; }
        public string email { get; set; }
        public string id { get; set; }
        // Ajouter le rôle de l'utilisateur
        public string role { get; set; } // "Medecin" ou "Pharmacien"
    }
}
