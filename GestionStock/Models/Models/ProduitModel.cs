﻿using GestionStock.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models.Models
{
    public class ProduitModel
    {
        public Produit produit { get; set; }
        public Utilisateur utilisateur { get; set; }
    }
}
