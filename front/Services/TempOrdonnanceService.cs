using System;
using System.Collections.Generic;
using System.Linq;
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
            // Vérifier si la ligne existe déjà (même médicament)
            var existing = _tempLignes.FirstOrDefault(l => l.ligneID == ligne.ligneID);
            if (existing == null)
            {
                _tempLignes.Add(ligne);
            }
            else
            {
                UpdateLigne(ligne);
            }
        }

        // Mettre à jour une ligne
        public void UpdateLigne(LigneMedicament ligne)
        {
            var existing = _tempLignes.FirstOrDefault(l => l.ligneID == ligne.ligneID);
            if (existing != null)
            {
                existing.MedicamentID = ligne.MedicamentID;
                existing.Medicament = ligne.Medicament;
                existing.qtePrescrite = ligne.qtePrescrite;
                existing.qteDelivre = ligne.qteDelivre;
                existing.dose = ligne.dose;
                existing.dateDelivre = ligne.dateDelivre;
                existing.statut = ligne.statut;
            }
        }

        // Supprimer une ligne
        public void RemoveLigne(LigneMedicament ligne)
        {
            _tempLignes.RemoveAll(l => l.ligneID == ligne.ligneID);
        }

        // Récupérer toutes les lignes
        public List<LigneMedicament> GetLignes()
        {
            return _tempLignes;
        }

        // Récupérer une ligne par son ID
        public LigneMedicament GetLigneParId(int ligneID)
        {
            return _tempLignes.FirstOrDefault(l => l.ligneID == ligneID);
        }
        // initialiser tout
        public void SetLignes(List<LigneMedicament> lignes)
        {
            _tempLignes = lignes ?? new List<LigneMedicament>();
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
