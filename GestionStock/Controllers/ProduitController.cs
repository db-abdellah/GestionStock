﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GestionStock.Handlers;
using GestionStock.Models.Business;
using GestionStock.Models.Business.Imp;
using GestionStock.Models.Entities;
using GestionStock.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GestionStock.Controllers
{
    public class ProduitController : Controller
    {
        private ProduitBusiness produitBusiness = new ProduitBusinessImp();
        private StockBusiness stockBusiness = new StockBusinessImp();
      
        private IHostEnvironment _env;

       
        public ProduitController(IHostEnvironment env)
        {
            _env = env;

        }
        // GET: ProduitController
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            ProduitsModel model = new ProduitsModel();
            model.produits = produitBusiness.getProduits();
            model.utilisateur = GetChefFromCookie();
            return View(model);
        }

        [VerifyUserAttribute]
        public ActionResult Group(String group)
        {
            ProduitsModel model = new ProduitsModel();
            model.produits = produitBusiness.getProduitsByGroup(group);
            model.utilisateur = GetChefFromCookie();
            return View(model);
        }

        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            ProduitModel model = new ProduitModel();
            model.utilisateur = GetChefFromCookie();
            model.produit = produitBusiness.getProduitById(id);
            model.produit.qteEstime = stockBusiness.getQteEstime(id);
            return View(model);
        }

        // GET: ProduitController/Create
        [VerifyUserAttribute]
        public ActionResult Create()
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            ViewBag.groups = produitBusiness.getProduitsAndAtelierStock().groups;
            ViewBag.categorie = produitBusiness.getProduitsAndAtelierStock().categories;
            return View();
        }

        [VerifyUserAttribute]
        public ActionResult CreateGroup()
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            ViewBag.categorie = produitBusiness.getProduitsAndAtelierStock().categories;

            return View();
        }


        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
        public ActionResult Create(Produit produit, IFormFile file)
        {
            produit = clearInput(produit);
            produit.groupProduit = "Null";
            Utilisateur util = GetChefFromCookie();
                

            ViewBag.utilisateur = util;
            int idProduit = produitBusiness.saveProduit(produit);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Creation Produit : " + produit.nom);
            if (file != null)
            {

                var dir = _env.ContentRootPath + @"/wwwroot/images/Produits";

                using (var filestream = new FileStream(Path.Combine(dir, idProduit +".jpeg"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(filestream);

                }
            }
                return RedirectToAction(nameof(Index));
            
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
        public ActionResult CreateGroup(Produit produit, IFormFile file,int min, int max)
        {
     
            produit.qteEstime = 0;
            produit = clearInput(produit);

            float prixAchat = float.Parse(produit.prixAchat, CultureInfo.InvariantCulture.NumberFormat)/ min;
            float prixVente = float.Parse( produit.prixVente, CultureInfo.InvariantCulture.NumberFormat) / min;
            Utilisateur util = GetChefFromCookie();

            String nom = produit.groupProduit;
            ViewBag.utilisateur = util;
            for(int i = min; i < max + 1; i++)
            {
                produit.prixAchat = (prixAchat * i).ToString();
                produit.prixVente = (prixVente * i).ToString();
                produit.nom =nom+ "  " + i + "M";
           
            int idProduit = produitBusiness.saveProduit(produit);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Creation Produit : " + produit.nom);
            if (file != null)
            {

                var dir = _env.ContentRootPath + @"/wwwroot/images/Produits";

                using (var filestream = new FileStream(Path.Combine(dir, idProduit + ".jpeg"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(filestream);

                }
            }
            }
            return RedirectToAction(nameof(Index));

        }




        // GET: ProduitController/Edit/5
        [VerifyUserAttribute]
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Produit produit = produitBusiness.getProduitById(id);
            produit.qteEstime = stockBusiness.getQteEstime(id);
            ViewBag.groups = produitBusiness.getProduitsAndAtelierStock().groups;
            ViewBag.categorie = produitBusiness.getProduitsAndAtelierStock().categories;
            return View(produit);
        }

        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
        public ActionResult Edit(Produit produit, IFormFile file)
        {
            try
            {
                if (file != null)
                {

                    var dir = _env.ContentRootPath + @"/wwwroot/images/Produits";

                    using (var filestream = new FileStream(Path.Combine(dir, produit.id + ".jpeg"), FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(filestream);

                    }
                }
                Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                produitBusiness.updateProduit(produit);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProduitController/Delete/5
        [VerifyUserAttribute]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProduitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult SupprimerProduit(int idProduit)
        {


            produitBusiness.DeleteProduitById(idProduit);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression Produit " );
            return Json("true");




        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult SupprimerGroup(String group)
        {


            produitBusiness.DeleteProduitByGroup(group);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression Group  :  "+group);
            return Json("true");




        }

        private Produit clearInput(Produit prod)
        {
            prod.idDeProduit = prod.idDeProduit.Replace("\'", " ");
            prod.nom = prod.nom.Replace("\'", " ");
            prod.groupProduit = prod.groupProduit.Replace("\'", " ");
            prod.categorie = prod.categorie.Replace("\'", " ");
            return prod;
        }





        //----------------------------------------------------------------------
        [VerifyUserAttribute]
        private Utilisateur GetChefFromCookie()
        {
            var jsonResult = HttpContext.Session.GetString("administrateur");
            if (jsonResult == null)
                jsonResult = HttpContext.Session.GetString("magasinier");
          
            return JsonConvert.DeserializeObject<Utilisateur>(jsonResult);
        }
        //----------------------------------------------------------------------

        public class VerifyUserAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var operateur = filterContext.HttpContext.Session.GetString("operateur");
                var magasinier = filterContext.HttpContext.Session.GetString("magasinier");
                var admin = filterContext.HttpContext.Session.GetString("administrateur");

                if ( magasinier == null && admin == null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }
    }
}
