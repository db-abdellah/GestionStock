using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Entities
{
    public class Notification
    {
        public int id { get; set; }
        public String idUser { get; set; }
        public String Date { get; set; }
        public String text { get; set; }

    }
}
