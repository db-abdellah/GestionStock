using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface ClientDao
    {
        List<Client> GetClients();
        Client GetClientById(int id);
        void saveClient(Client Client);
        void updateClient(Client Client);
        void DeleteClientById(int idClient);
    }
}
