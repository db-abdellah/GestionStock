using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class AchatBusinessImp : AchatBusiness
    {
        private static AchatDao achatDao = new AchatDaoImp();

        public void saveDetails(float total, int idFacture, int idProduit, int qte)
        {
            achatDao.saveDetails( total,  idFacture,  idProduit,qte);
        }

        public int saveFacture(float totalFacture, string numDocument, int fournisseurId)
        {
            return achatDao.saveFacture(totalFacture, numDocument, fournisseurId);
        }
    }
}
