using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface AchatBusiness
    {
        int saveFacture(float totalFacture, string numDocument, int fournisseurId);
        void saveDetails(float total, int idFacture, int idProduit, int qte);
    }
}
