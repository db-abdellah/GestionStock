using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class TransactionListModel
    {
        public Utilisateur util { get; set; }
        public List<TransactionsModel> list { set; get; }
    }
}
