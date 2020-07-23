using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class AchatProduitModel
    {
        public Produit produit { get; set; }
        public Stock stock { get; set; }
    }
}
