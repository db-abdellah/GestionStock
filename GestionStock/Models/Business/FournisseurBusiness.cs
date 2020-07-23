using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface FournisseurBusiness
    {
        List<Fournisseur> getFournisseurs();
        Fournisseur getFournisseurById(int id);
        void saveFournisseur(Fournisseur fournisseur);
        void updateFournisseur(Fournisseur fournisseur);
    }
}
