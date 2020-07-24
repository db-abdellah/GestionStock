using GestionStock.Models.DAO;
using GestionStock.Models.DAO.Imp;
using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business.Imp
{
    public class UtilisateurBusinessImp : UtilisateurBusiness

    {
        UtilisateurDao utilDao = new UtilisateurDaoImp();

        public int checkNewLogin(string login)
        {
            return utilDao.checkNewLogin( login);
        }

        public Utilisateur getUtilisateurById(int id)
        {
            return utilDao.getUtilisateurById(id);
        }

        public List<Utilisateur> getUtilisateurs()
        {
            return utilDao.getUtilisateurs();
        }

        public void saveUtilisateur(Utilisateur utilisateur)
        {
            utilDao.saveUtilisateur(utilisateur);
        }

        public void updateUtilisateur(Utilisateur utilisateur)
        {
            utilDao.updateUtilisateur(utilisateur);
        }

        Utilisateur UtilisateurBusiness.connecterUtilisateur(string username, string password)
        {

            Utilisateur utilisateur = utilDao.GetUtilisateurByLogin(username, password);

            return utilisateur;
        }
    }
}
