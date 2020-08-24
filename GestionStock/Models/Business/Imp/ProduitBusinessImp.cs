using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using GestionStock.Models.Entities;
using GestionStock.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class ProduitBusinessImp : ProduitBusiness
    {
        private ProduitDao produitDao = new ProduitDaoImp();

        public void DeleteProduitById(int idProduit)
        {
            produitDao.DeleteProduitById(idProduit);
        }

        public Produit getProduitById(int idProjet)
        {
            return produitDao.getProduitById(idProjet);
        }

        public List<Produit> getProduits()
        {
            return produitDao.getProduits();
        }

        public ESModel getProduitsAndAtelierStock()
        {
            return produitDao.getProduitsAndAtelierStock();
        }

        public ESModel getProduitsAndStock()
        {
            return produitDao.getProduitsAndStock();
        }

        public List<Produit> getProduitsByGroup(string group)
        {
            return produitDao.getProduitsByGroup( group);
        }

        public int saveProduit(Produit produit)
        {
           return produitDao.saveProduit(produit);
        }

        public void updateProduit(Produit produit)
        {
            produitDao.updateProduit(produit);
        }
    }
}
