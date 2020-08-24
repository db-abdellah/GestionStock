using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    public interface StockBusiness
    {
        List<Stock> getAllStock();
        Stock getStockByProduitId(int idProduit);
        Stock getStockById(int idStock);
        void UpdateQteReel(int idStock, int quantite);
        List<Stock> getAllStockNotifications();
        List<Stock> getAllStockAlerts();
        int getQteEstime(int id);
    }
}
