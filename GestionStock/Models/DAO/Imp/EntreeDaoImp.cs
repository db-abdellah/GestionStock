using Dapper;
using GestionStock.Handlers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO.Imp
{
    public class EntreeDaoImp : EntreeDao
    {
        public void EntreeAtelier(int idProduit, int qte)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO es(idAtelier,type,Qte,idProduit,date) VALUES(1,'E','{qte}','{idProduit}',CURRENT_TIMESTAMP) ";
                connection.Execute(query);
                query =
                    $"UPDATE stock  SET QteEstimee = QteEstimee - {qte}  WHERE idProduit = {idProduit} ";
                connection.Execute(query);




            }
        }

        public void SortieAtelier(int idProduit, int qte)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO es(idAtelier,type,Qte,idProduit,date) VALUES(1,'S','{qte}','{idProduit}',CURRENT_TIMESTAMP) ";
                connection.Execute(query);
                query =
                    $"UPDATE stock  SET QteEstimee = QteEstimee + {qte}  WHERE idProduit = {idProduit} ";
                connection.Execute(query);




            }
        }
    }
}
