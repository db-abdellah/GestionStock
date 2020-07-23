using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class AchatModel
    {
        public Utilisateur utilisateur { get; set; }
        public List<Fournisseur> fournisseurList { get; set; }
        public List<Produit> ProduitList { get; set; }
    }
}
