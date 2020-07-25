using GestionStock.Models.DAO;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class StockBusinessImp : StockBusiness
    {
        private static StockDao stockDao = new StockDaoImp();
        public List<Stock> getAllStock()
        {
            return stockDao.getAllStock();
        }

        public Stock getStockById(int idStock)
        {
            return stockDao.getStockById( idStock);
        }

        public Stock getStockByProduitId(int idProduit)
        {
            return stockDao.getStockByProduitId( idProduit);
        }

        public void UpdateQteReel(int idStock, int quantite)
        {
            stockDao.UpdateQteReel( idStock,  quantite);
        }
    }
}
