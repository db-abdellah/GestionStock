using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class ProduitsModel
    {
        public List<Produit> produits { get; set; }
        public Utilisateur utilisateur { get; set; }
    }
}
