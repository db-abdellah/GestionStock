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
        public List<Document> getAllDocuments()
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT document.id,document.idFournisseur,document.numero,fournisseur.nom as nomFournisseur,document.total,document.date FROM document,fournisseur WHERE document.idFournisseur = fournisseur.id ";
                List<Document> documents = connection.Query<Document>(query).ToList();



                return documents;
            }
        }
    }
}
