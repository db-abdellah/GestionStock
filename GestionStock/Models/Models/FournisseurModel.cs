using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class FournisseurModel
    {
        public Utilisateur utilisateur { get; set; }
        public Fournisseur fournisseur { get; set; }
    }
}
