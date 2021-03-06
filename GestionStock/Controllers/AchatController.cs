﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStock.Handlers;
using GestionStock.Models.Business;
using GestionStock.Models.Business.Imp;
using GestionStock.Models.Entities;
using GestionStock.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GestionStock.Controllers
{
    public class AchatController : Controller
    {
        private static FournisseurBusiness fournisseurBusiness = new FournisseurBusinessImp();
        private static ProduitBusiness produitBusiness = new ProduitBusinessImp();
        private static StockBusiness stockBusiness = new StockBusinessImp();
        private static AchatBusiness achatBusiness = new AchatBusinessImp();
        private static DocumentBusiness documentBusiness = new DocumentBusinessImp();

        private IHostEnvironment _env;


        public AchatController(IHostEnvironment env)
        {
            _env = env;

        }

        [VerifyUserAttribute]
        // GET: AchatController
        public ActionResult Index()
        {
            List<Document> documentList = documentBusiness.getAllAchats();
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View(documentList);
        }
        [VerifyUserAttribute]
        public ActionResult Achat()
        {
            AchatModel model = new AchatModel();
            model.utilisateur = GetChefFromCookie();
            model.fournisseurList = fournisseurBusiness.getFournisseurs();
            model.ProduitList = produitBusiness.getProduits2();

            return View(model);
        }

        [VerifyUserAttribute]
        [HttpPost]
        public JsonResult facture(string totalFacture, string numDocument, int fournisseurId)
        {
            numDocument = numDocument.Replace("\'", " ");
            int idFacture = achatBusiness.saveFacture(totalFacture, numDocument, fournisseurId);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Achat document :  " + numDocument);
            return Json(idFacture);


        }
        [VerifyUserAttribute]
        [HttpPost]
        public JsonResult factureDetails(String total, int idFacture, int idProduit, int quantite)
        {
            achatBusiness.saveDetails(total, idFacture, idProduit, quantite);
            return Json("true");


        }

        [VerifyUserAttribute]
        // GET: AchatController/Details/5
        public ActionResult Details(int id)
        {
            FactureModel model = new FactureModel();
            model.utilisateur = GetChefFromCookie();
            model.document = documentBusiness.getDocumentById(id);
            model.achatList = achatBusiness.getAchatByDocumentId(id);
            return View(model);
        }

        [VerifyUserAttribute]
        // Partial View 
        public ActionResult AchatProduit(int idProduit)
        {
            AchatProduitModel model = new AchatProduitModel();
            model.produit = produitBusiness.getProduitById(idProduit);
            model.stock = stockBusiness.getStockByProduitId(idProduit);
            return PartialView(model);
        }
      
        // Partial View 
        public ActionResult VenteProduit(int idProduit)
        {
            AchatProduitModel model = new AchatProduitModel();
            model.produit = produitBusiness.getProduitById(idProduit);
            model.stock = stockBusiness.getStockByProduitId(idProduit);
            return PartialView(model);
        }
        [VerifyUserAttribute]
        // POST: AchatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        [VerifyUserAttribute]
        // GET: AchatController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [VerifyUserAttribute]
        // POST: AchatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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


        [VerifyUserAttribute]
        [HttpPost]
        public JsonResult SupprimerDocument(int idDocument)
        {


            documentBusiness.DeleteDocumentById(idDocument);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression de document");
            return Json("true");




        }

        [VerifyUserAttribute]
        // POST: AchatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        //----------------------------------------------------------------------
        [VerifyUserAttribute]
        private Utilisateur GetChefFromCookie()
        {
            var jsonResult = HttpContext.Session.GetString("administrateur");
            if (jsonResult == null)
                jsonResult = HttpContext.Session.GetString("magasinier");
            if (jsonResult == null)
                jsonResult = HttpContext.Session.GetString("operateur");
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

                if (magasinier == null && admin == null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }
    }
}