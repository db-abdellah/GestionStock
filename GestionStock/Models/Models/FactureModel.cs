using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class FactureModel
    {
        public Utilisateur utilisateur { set; get; }
        public Document document { set; get; }
        public List<Achat> achatList { set; get; }
    }
}
