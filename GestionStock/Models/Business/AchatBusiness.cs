using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface AchatBusiness
    {
        int saveFacture(string totalFacture, string numDocument, int fournisseurId);
        void saveDetails(string total, int idFacture, int idProduit, int qte);
        List<Achat> getAchatByDocumentId(int id);
    }
}
