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
    public class ClientDaoImp : ClientDao
    {
        public void DeleteClientById(int idClient)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"DELETE FROM client WHERE id = {idClient}; ";
                connection.Execute(query);





            }
        }

        public Client GetClientById(int id)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT * FROM client   WHERE id={id}";
                List<Client> Clients = connection.Query<Client>(query).ToList();



                return Clients[0];
            }
        }

        public List<Client> GetClients()
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query = $"SELECT * FROM client   ";
                List<Client> Clients = connection.Query<Client>(query).ToList();



                return Clients;
            }
        }

        public void saveClient(Client Client)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"INSERT INTO client(nom,Prenom,adresse,telephone) VALUES('{Client.Nom}','{Client.prenom} ','{Client.adresse}','{Client.telephone}') ";
                connection.Execute(query);




            }
        }

        public void updateClient(Client Client)
        {
            using (IDbConnection connection = ConnectionHandler.Instance.getConnection())
            {
                String query =
                    $"UPDATE client  SET nom = '{Client.Nom}', Prenom = '{Client.prenom}',adresse= '{Client.adresse}', telephone= '{Client.telephone}' WHERE id = {Client.id} ";
                connection.Execute(query);




            }
        }
    }
}
