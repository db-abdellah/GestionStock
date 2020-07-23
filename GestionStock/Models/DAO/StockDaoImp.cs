using Dapper;
using GestionStock.Handlers;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    public class StockDaoImp : StockDao
    {
        public List<Stock> getAllStock()
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT stock.id ,stock.QteReel,stock.QteEstimee ,produit.nom FROM stock ,produit WHERE produit.id=stock.idProduit ";
                List<Stock> stockList = connection.Query<Stock>(query).ToList();
                return stockList;
            }
        }

        public Stock getStockById(int idStock)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT stock.id ,stock.QteReel,stock.QteEstimee ,produit.nom FROM stock ,produit WHERE produit.id=stock.idProduit AND stock.id={idStock}";
                List<Stock> stock = connection.Query<Stock>(query).ToList();



                return stock[0];
            }
        }

        public Stock getStockByProduitId(int idProduit)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT stock.id ,stock.QteReel,stock.QteEstimee ,produit.nom FROM stock ,produit WHERE produit.id=stock.idProduit AND stock.idProduit={idProduit}";
                List<Stock> stock = connection.Query<Stock>(query).ToList();

                return stock[0];
            }
        }
    }
}
