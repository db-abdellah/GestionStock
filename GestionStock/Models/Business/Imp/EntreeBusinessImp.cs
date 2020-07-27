using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class EntreeBusinessImp : EntreeBusiness
    {
        private static EntreeDao entreeDao = new EntreeDaoImp();
        public void EntreeAtelier(int idProduit, int qte)
        {
            entreeDao.EntreeAtelier(idProduit, qte);
        }

        public void SortieAtelier(int idProduit, int qte)
        {
            entreeDao.SortieAtelier(idProduit, qte);
        }
    }
}
