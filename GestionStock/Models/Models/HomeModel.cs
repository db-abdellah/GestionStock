using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class HomeModel
    {
        public Utilisateur utilisateur { get; set; }
        public List<Stock> stockList { get; set; }
        public List<Stock> AlertStockList { get; set; }
    }
}
