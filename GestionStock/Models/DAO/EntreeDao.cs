using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface EntreeDao
    {
        void EntreeAtelier(int idProduit, int qte);
        void SortieAtelier(int idProduit, int qte);
    }
}
