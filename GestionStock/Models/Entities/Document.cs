using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class Document
    {
        public int id { get; set; }
        public int idFournisseur { get; set; }
        public String numero { get; set; }
        public String nomFournisseur { get; set; }
        public float total { get; set; }
        public String  date { get; set; }
    }
}
