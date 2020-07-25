using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface FournisseurDao
    {
        List<Fournisseur> GetFournisseurs();
        Fournisseur GetFournisseurById( int id);
        void saveFournisseur(Fournisseur fournisseur);
        void updateFournisseur(Fournisseur fournisseur);
        void DeleteFournisseurById(int idFournisseur);
    }
}
