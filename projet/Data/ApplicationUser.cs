using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using projet.DTO;
namespace projet.Data
{
    public class ApplicationUser : IdentityUser
    {
        
        public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Tel { get; set; }
    public string Adresse { get; set; }
    public string? Specialite { get; set; }
    public string? NomPharmacie { get; set; }
        // ⚠️ On ne l'appelle plus "Role" car conflit avec Identity !
        public Role UserRole { get; set; }

    }
}
//UserManager<ApplicationUser> ?

//C’est une classe d’Identity qui contient toutes les méthodes utiles pour gérer les utilisateurs :

//🔹 Création d’un utilisateur
//await userManager.CreateAsync(user, password);

//🔹 Trouver un utilisateur par ID
//await userManager.FindByIdAsync(id);

//Trouver un utilisateur par email ou username
//await userManager.FindByEmailAsync(email);
//await userManager.FindByNameAsync(username);

//🔹 Vérifier un mot de passe
//await userManager.CheckPasswordAsync(user, password);

//🔹 Modifier un utilisateur
//await userManager.UpdateAsync(user);

//🔹 Ajouter un utilisateur à un rôle
//await userManager.AddToRoleAsync(user, "Admin");


//👉 Donc: userManger = le service qui gère la table des utilisateurs.