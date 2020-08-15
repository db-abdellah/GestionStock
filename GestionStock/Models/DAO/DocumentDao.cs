using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface DocumentDao
    {
        List<Document> getAllAchats();
        void DeleteDocumentById(int idDocument);
        Document getDocumentById(int id);
         List<Document> getAllVentes();
    }
}
