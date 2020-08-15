using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class ClientBusinessImp : ClientBusiness
    {
       
            private ClientDao ClientDao = new ClientDaoImp();

            public void DeleteClientById(int idClient)
            {
                ClientDao.DeleteClientById(idClient);
            }

      

        public Client getClientById(int id)
            {
                return ClientDao.GetClientById(id);
            }

            public List<Client> getClients()
            {

                return ClientDao.GetClients();
            }

            public void saveClient(Client Client)
            {
                ClientDao.saveClient(Client);
            }

            public void updateClient(Client Client)
            {
                ClientDao.updateClient(Client);
            }

        }
    }
