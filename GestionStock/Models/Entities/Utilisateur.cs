using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class Utilisateur
    {
        public int id { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public String login { get; set; }
        public String password { get; set; }
        public String adresse { get; set; }
        public String telephone { get; set; }
        public String date { get; set; }
        public String fonction { get; set; }
    }
}
