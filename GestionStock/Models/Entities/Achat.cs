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
        public String idFournisseur { get; set; }
        public int qte { get; set; }
    }
}
