using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class Client
    {
        public int id { get; set; }
        public String Nom { get; set; }
        public String prenom { get; set; }
        public String adresse { get; set; }
        public String telephone { get; set; }
    }
}
