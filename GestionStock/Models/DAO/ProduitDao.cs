﻿using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.DAO
{
    interface ProduitDao
    {
        List<Produit> getProduits();
        Produit getProduitById(int idProjet);
        void saveProduit(Produit produit);
        void updateProduit(Produit produit);
    }
}