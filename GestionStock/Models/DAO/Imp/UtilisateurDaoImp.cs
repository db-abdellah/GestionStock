﻿using Dapper;
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

        public void saveUtilisateur(Utilisateur util)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO utilisateur(nom,prenom,login,password,adresse,telephone,fonction,date) VALUES('{util.nom}','{util.prenom} ','{util.login}','{util.password}', '{util.adresse}', '{util.telephone}', '{util.fonction}', CURRENT_TIMESTAMP) ";
                connection.Execute(query);




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