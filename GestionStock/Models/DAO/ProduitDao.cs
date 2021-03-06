﻿using GestionStock.Models.Entities;
using GestionStock.Models.Models;
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
        int saveProduit(Produit produit);
        void updateProduit(Produit produit);
        void DeleteProduitById(int idProduit);
        ESModel getProduitsAndStock();
        void DeleteProduitByGroup(string group);
        ESModel getProduitsAndAtelierStock();
        List<Produit> getProduitsByGroup(string group);
        Produit getProduitByIdProduit(string idProduit);
        List<Produit> getAllProduits();
        List<Produit> getProduits2();
    }
}
