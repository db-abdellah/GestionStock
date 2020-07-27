using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class LogModel
    {
        public Utilisateur util { set; get; } 
        public List<Utilisateur> logList { set; get; }
    }
}
