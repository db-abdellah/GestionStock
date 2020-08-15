using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface ClientBusiness
    {
        List<Client> getClients();
        Client getClientById(int id);
        void saveClient(Client client);
        void updateClient(Client client);
        void DeleteClientById(int idClient);
    }
}
