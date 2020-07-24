using Dapper;
using GestionStock.Handlers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO.Imp
{
    public class AchatDaoImp : AchatDao
    {
        public void saveDetails(float total, int idFacture, int idProduit,int qte)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"INSERT INTO achat(idFacture, qte,idProduit, total) VALUES('{idFacture}',{ qte},{idProduit},{ total}); ";
                connection.Execute(query);
            }
        }

        public int saveFacture(float totalFacture, string numDocument, int fournisseurId)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"INSERT INTO document(numero, idFournisseur	, total,date) VALUES('{numDocument}',{fournisseurId},{totalFacture}, CURRENT_TIMESTAMP); SELECT LAST_INSERT_ID() as id; ";
                dynamic result = connection.Query(query).First();


                int idProjet = (int)result.id;
                return idProjet;
            }
        }
    }
}
