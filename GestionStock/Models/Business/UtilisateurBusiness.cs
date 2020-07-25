using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Business
{
    interface UtilisateurBusiness
    {
        Utilisateur connecterUtilisateur(string username, string password);
        List<Utilisateur> getUtilisateurs();
        Utilisateur getUtilisateurById(int id);
        void saveUtilisateur(Utilisateur utilisateur);
        void updateUtilisateur(Utilisateur utilisateur);
        int checkNewLogin(string login);
        void DeleteUserById(int idUtil);
    }
}
