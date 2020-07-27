using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface AchatDao
    {
        int saveFacture(String totalFacture, string numDocument, int fournisseurId);
        void saveDetails(String total, int idFacture, int idProduit,int qte);
        List<Achat> getAchatByDocumentId(int id);
    }
}
