using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface StockBusiness
    {
        List<Stock> getAllStock();
        Stock getStockByProduitId(int idProduit);
        Stock getStockById(int idStock);
    }
}
