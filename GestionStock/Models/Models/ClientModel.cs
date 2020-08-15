using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class ClientModel
    {
        public Utilisateur utilisateur { get; set; }
        public Client client { get; set; }
    }
}
