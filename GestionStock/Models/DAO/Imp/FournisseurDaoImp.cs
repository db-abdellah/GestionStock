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
    public class FournisseurDaoImp : FournisseurDao
    {
        public Fournisseur GetFournisseurById(int id)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT * FROM fournisseur   WHERE id={id}";
                List<Fournisseur> fournisseurs = connection.Query<Fournisseur>(query).ToList();



                return fournisseurs[0];
            }
        }

        public List<Fournisseur> GetFournisseurs()
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT * FROM fournisseur   ";
                List<Fournisseur> fournisseurs = connection.Query<Fournisseur>(query).ToList();


              
                    return fournisseurs;
            }
        }

        public void saveFournisseur(Fournisseur fournisseur)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO fournisseur(nom,Prenom,adresse,telephone) VALUES('{fournisseur.Nom}','{fournisseur.prenom} ','{fournisseur.adresse}','{fournisseur.telephone}') ";
                connection.Execute(query);




            }
        }

        public void updateFournisseur(Fournisseur fournisseur)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"UPDATE fournisseur  SET nom = '{fournisseur.Nom}', Prenom = '{fournisseur.prenom}',adresse= '{fournisseur.adresse}', telephone= '{fournisseur.telephone}' WHERE id = {fournisseur.id} ";
                connection.Execute(query);




            }
        }
    }
}
