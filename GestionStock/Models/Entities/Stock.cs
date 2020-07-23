using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class Stock
    {
        public int id { get; set; }
        public int idProduit { get; set; }
        public int qteReel { get; set; }
        public int qteEstimee { get; set; }
    }
}
