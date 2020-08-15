using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    
    public class DocumentBusinessImp : DocumentBusiness
    {
        private static  DocumentDao documentDao = new DocumentDaoImp();

        public void DeleteDocumentById(int idDocument)
        {
            documentDao.DeleteDocumentById(idDocument);
        }

        public List<Document> getAllAchats()
        {
            return documentDao.getAllAchats();
        }

        public List<Document> getAllVentes()
        {
            return documentDao.getAllVentes();
        }

        public Document getDocumentById(int id)
        {
           return documentDao.getDocumentById( id);
        }
    }
}
