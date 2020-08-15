using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class VenteModel
    {
        public Utilisateur utilisateur { get; set; }
        public List<Client> ClientList { get; set; }
        public List<Produit> ProduitList { get; set; }
      
    }
}
