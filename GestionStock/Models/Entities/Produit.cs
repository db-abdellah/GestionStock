using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GestionStock.Models.Entities
{
    public class Produit
    {
        public int id { get; set; }
        public String categorie { get; set; }
        public String nom { get; set; }
        public String  prixAchat { get; set; }
        public String groupProduit { get; set; }

        public int qteEstime { get; set; }
        public IFormFile image { get; set; }
    }
}
