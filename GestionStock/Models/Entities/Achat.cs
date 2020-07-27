using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class Achat
    {
        public int id { get; set; }
        public int  idProduit { get; set; }
        public String  prixAchat { get; set; }
        public String  nomProduit { get; set; }
        public String idFournisseur { get; set; }
        public int qte { get; set; }
        public string total { get; set; }
    }
}
