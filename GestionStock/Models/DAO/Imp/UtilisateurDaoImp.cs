using Dapper;
using GestionStock.Handlers;
using GestionStock.Models.Entities;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO.Imp
{
    public class UtilisateurDaoImp : UtilisateurDao
    {
        public int checkNewLogin(string login)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {

                List<Utilisateur> util = connection.Query<Utilisateur>($"SELECT id FROM utilisateur WHERE login='{login}' ;").ToList();
                if(util.Count>=1)
                     return 1;

                return 0;
            }
        }

        public void DeleteUserById(int idUtil)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"DELETE FROM utilisateur WHERE id = {idUtil}; ";
                connection.Execute(query);
                




            }
        }

        public Utilisateur getUtilisateurById(int id)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {

                List<Utilisateur> util = connection.Query<Utilisateur>($"SELECT * FROM utilisateur WHERE id={id} ").ToList();
                return util[0];
            }
        }

        public Utilisateur GetUtilisateurByLogin(string username, string password)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {

                List<Utilisateur> util = connection.Query<Utilisateur>($"SELECT * FROM utilisateur WHERE  login = '{username}' AND password ='{password}' ").ToList();
                Console.WriteLine(util.Count)
                    ;
                if (util.Count > 0)
                    return util[0];
                else return null;
            }
        }

        public List<Utilisateur> getUtilisateurs()
        {

            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {

                List<Utilisateur> util = connection.Query<Utilisateur>($"SELECT * FROM utilisateur  ").ToList();

                return util;
               
            }
        }

        public int saveUtilisateur(Utilisateur util)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO utilisateur(nom,prenom,login,password,adresse,telephone,fonction,date) VALUES('{util.nom}','{util.prenom} ','{util.login}','{util.password}', '{util.adresse}', '{util.telephone}', '{util.fonction}', CURRENT_TIMESTAMP); SELECT LAST_INSERT_ID() as id; ";
                connection.Execute(query);

                dynamic result = connection.Query(query).First();


                int idUtilisateur = (int)result.id;

                return idUtilisateur;
            }
        }

        public void updateUtilisateur(Utilisateur utilisateur)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"UPDATE utilisateur  SET nom = '{utilisateur.nom}', prenom = '{utilisateur.prenom}',adresse= '{utilisateur.adresse}', telephone= '{utilisateur.telephone}', login= '{utilisateur.login}',password= '{utilisateur.password}',fonction= '{utilisateur.fonction}' WHERE id = {utilisateur.id} ";
                connection.Execute(query);




            }
        }
    }
}
