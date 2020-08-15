using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class AchatBusinessImp : AchatBusiness
    {
        private static AchatDao achatDao = new AchatDaoImp();

        public List<Achat> getAchatByDocumentId(int id)
        {
            return achatDao.getAchatByDocumentId( id);
        }

        public void saveDetails(string total, int idFacture, int idProduit, int qte)
        {
            achatDao.saveDetails( total,  idFacture,  idProduit,qte);
        }

        public void saveDetailsVente(string total, int idFacture, int idProduit, int quantite)
        {
            achatDao.saveDetailsVente(total, idFacture, idProduit, quantite);
        }

        public int saveFacture(string totalFacture, string numDocument, int fournisseurId)
        {
            return achatDao.saveFacture(totalFacture, numDocument, fournisseurId);
        }

        public int saveVente(string totalFacture, string numDocument, int fournisseurId)
        {
            return achatDao.saveVente(totalFacture, numDocument, fournisseurId);
        }
    }
}
