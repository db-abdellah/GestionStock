using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface UtilisateurDao
    {

        Utilisateur GetUtilisateurByLogin(String username, String password);
        List<Utilisateur> getUtilisateurs();
        Utilisateur getUtilisateurById(int id);
        int saveUtilisateur(Utilisateur utilisateur);
        void updateUtilisateur(Utilisateur utilisateur);
        int checkNewLogin(string login);
        void DeleteUserById(int idUtil);
    }
}
