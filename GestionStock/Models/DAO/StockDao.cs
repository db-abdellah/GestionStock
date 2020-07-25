using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface StockDao
    {
        List<Stock> getAllStock();
        Stock getStockById(int idStock);
        Stock getStockByProduitId(int idProduit);
        void UpdateQteReel(int idStock, int quantite);
    }
}
