using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class FournisseurBusinessImp : FournisseurBusiness
    {   
        private FournisseurDao  fournisseurDao=  new FournisseurDaoImp();

        public Fournisseur getFournisseurById( int id)
        {
            return fournisseurDao.GetFournisseurById(id);
        }

        public List<Fournisseur> getFournisseurs()
        {
            
            return fournisseurDao.GetFournisseurs();
        }

        public void saveFournisseur(Fournisseur fournisseur)
        {
            fournisseurDao.saveFournisseur(fournisseur);
        }

        public void updateFournisseur(Fournisseur fournisseur)
        {
            fournisseurDao.updateFournisseur(fournisseur);
        } 

    }
}
