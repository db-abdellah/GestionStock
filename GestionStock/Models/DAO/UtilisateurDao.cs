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
        void saveUtilisateur(Utilisateur utilisateur);
        void updateUtilisateur(Utilisateur utilisateur);
    }
}
