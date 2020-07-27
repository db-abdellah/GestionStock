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
    public class DocumentDaoImp : DocumentDao
    {
        public void DeleteDocumentById(int idDocument)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"DELETE FROM document WHERE id = {idDocument}; ";
                connection.Execute(query);
                 query = $"DELETE FROM achat WHERE idFacture = {idDocument}; ";
                connection.Execute(query);




            }
        }

        public List<Document> getAllDocuments()
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT document.id,document.idFournisseur,document.numero,fournisseur.nom as nomFournisseur,document.total,document.date FROM document,fournisseur WHERE document.idFournisseur = fournisseur.id ";
                List<Document> documents = connection.Query<Document>(query).ToList();



                return documents;
            }
        }

        public Document getDocumentById(int id)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT document.id,document.idFournisseur,document.numero,fournisseur.Prenom as prenomFournisseur , fournisseur.nom as nomFournisseur ,document.total,document.date FROM document,fournisseur WHERE document.idFournisseur = fournisseur.id AND document.id={id}";
                List<Document> documents = connection.Query<Document>(query).ToList();



                return documents[0];
            }
        }
    }
}
