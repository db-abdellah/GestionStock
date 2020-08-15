using Dapper;
using GestionStock.Handlers;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO.Imp
{
    public class AchatDaoImp : AchatDao
    {
        public List<Achat> getAchatByDocumentId(int id)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT achat.id,achat.idProduit,achat.idFacture,produit.nom as nomProduit, produit.prixAchat as prixAchat ,achat.qte,achat.total FROM achat,produit WHERE achat.idProduit = produit.id AND achat.idFacture = {id}";
                
                List<Achat> achats = connection.Query<Achat>(query).ToList();



                return achats;
            }
        }

        public void saveDetails(String total, int idFacture, int idProduit,int qte)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"INSERT INTO achat(idFacture, qte,idProduit, total) VALUES('{idFacture}',{ qte},{idProduit},{ total}); ";
                connection.Execute(query);
                query =
                     $"UPDATE stock  SET QteEstimee = QteEstimee + {qte}  WHERE idProduit = {idProduit} ";
                connection.Execute(query);
            }
        }
        public void saveDetailsVente(String total, int idFacture, int idProduit, int qte)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"INSERT INTO achat(idFacture, qte,idProduit, total) VALUES('{idFacture}',{ qte},{idProduit},{ total}); ";
                connection.Execute(query);
                query =
                     $"UPDATE stock  SET QteEstimee = QteEstimee - {qte}  WHERE idProduit = {idProduit} ";
                connection.Execute(query);
            }
        }

        public int saveFacture(String totalFacture, string numDocument, int fournisseurId)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"INSERT INTO document(numero, idFournisseur	, total,date,type) VALUES('{numDocument}',{fournisseurId},{totalFacture}, CURRENT_TIMESTAMP,'achat'); SELECT LAST_INSERT_ID() as id; ";
                dynamic result = connection.Query(query).First();


                int idProjet = (int)result.id;
                return idProjet;
            }
        }

        public int saveVente(String totalFacture, string numDocument, int fournisseurId)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"INSERT INTO document(numero, idFournisseur	, total,date,type) VALUES('{numDocument}',{fournisseurId},{totalFacture}, CURRENT_TIMESTAMP,'vente'); SELECT LAST_INSERT_ID() as id; ";
                dynamic result = connection.Query(query).First();


                int idProjet = (int)result.id;
                return idProjet;
            }
        }
    }
}
