using System;
using System.Collections.Generic;
using metiers;

namespace front.Services
{
    public class TempOrdonnanceService
    {
        // Ordonnance temporaire
        public Ordonnance TempOrdonnance { get; set; } = new Ordonnance { DateCreation = DateTime.Today };

        // Lignes de médicaments temporaires
        private List<LigneMedicament> _tempLignes = new List<LigneMedicament>();
        public List<LigneMedicament> TempLignes => _tempLignes;

        // Pharmacie temporaire sélectionnée
        public Guid? TempPharmacieID { get; set; } = null;

        // Ajouter une ligne
        public void AddLigne(LigneMedicament ligne)
        {
            _tempLignes.Add(ligne);
        }

        // Supprimer une ligne
        public void RemoveLigne(LigneMedicament ligne)
        {
            _tempLignes.Remove(ligne);
        }

        // Récupérer toutes les lignes
        public List<LigneMedicament> GetLignes()
        {
            return _tempLignes;
        }

        // Réinitialiser tout
        public void Clear()
        {
            TempOrdonnance = new Ordonnance { DateCreation = DateTime.Today };
            _tempLignes.Clear();
            TempPharmacieID = null;
        }
    }
}
