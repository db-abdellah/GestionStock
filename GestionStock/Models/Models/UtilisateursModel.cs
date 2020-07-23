using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class UtilisateursModel
    {
        public Utilisateur utilisateur { get; set; }
        public List<Utilisateur> utilisateurList { get; set; }
    }
}
