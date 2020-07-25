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
    public class ProduitDaoImp : ProduitDao
    {
        public void DeleteProduitById(int idProduit)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"DELETE FROM produit WHERE id = {idProduit}; ";
                connection.Execute(query);
                query = $"DELETE FROM stock WHERE idProduit = {idProduit}; ";
                connection.Execute(query);




            }
        }

        public Produit getProduitById(int idProjet)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {

                List<Produit> produits = connection.Query<Produit>($"SELECT * FROM produit  WHERE id={idProjet} ").ToList();

                return produits[0];

            }
        }

        public List<Produit> getProduits()
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {

                List<Produit> produits = connection.Query<Produit>($"SELECT * FROM produit  ").ToList();

                return produits;

            }
        }

        public int saveProduit(Produit produit)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO produit(nom,prixAchat,categorie) VALUES('{produit.nom}','{produit.prixAchat} ','{produit.categorie}'); SELECT LAST_INSERT_ID() as id;";
               
                dynamic result = connection.Query(query).First();


                int idProduit = (int)result.id;
                 query =
                    $"INSERT INTO stock(idProduit,QteEstimee) VALUES('{idProduit} ','{produit.qteEstime}');";
                connection.Execute(query);


                return idProduit;

            }
        }

        public void updateProduit(Produit produit)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"UPDATE produit  SET nom = '{produit.nom}', categorie = '{produit.categorie}',prixAchat= '{produit.prixAchat}' WHERE id = {produit.id} ";
                connection.Execute(query);




            }
        }
    }
}
