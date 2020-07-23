using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class ES
    {
        public int id { get; set; }
        public String idAtelier { get; set; }
        public String type { get; set; }
        public int qte { get; set; }
    }
}
