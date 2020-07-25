using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface DocumentDao
    {
        List<Document> getAllDocuments();
        void DeleteDocumentById(int idDocument);
    }
}
