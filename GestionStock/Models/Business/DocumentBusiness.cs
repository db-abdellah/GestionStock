using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface DocumentBusiness
    {
        List<Document> getAllDocuments();
        void DeleteDocumentById(int idProjet);
        Document getDocumentById(int id);
    }
}
