using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface EntreeBusiness
    {
        void EntreeAtelier(int idProduit, int qte);
        void SortieAtelier(int idProduit, int qte);
    }
}
