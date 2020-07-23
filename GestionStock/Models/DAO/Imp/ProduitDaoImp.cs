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

        public void saveProduit(Produit produit)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO produit(nom,prixAchat,categorie) VALUES('{produit.nom}','{produit.prixAchat} ','{produit.categorie}') ";
                connection.Execute(query);




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
