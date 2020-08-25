using System;
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
    public class VenteController : Controller
    {
        private static ClientBusiness clientBusiness = new ClientBusinessImp();
        private static ProduitBusiness produitBusiness = new ProduitBusinessImp();
        private static StockBusiness stockBusiness = new StockBusinessImp();
        private static AchatBusiness achatBusiness = new AchatBusinessImp();
        private static DocumentBusiness documentBusiness = new DocumentBusinessImp();

        private IHostEnvironment _env;

        public VenteController(IHostEnvironment env)
        {
            _env = env;

        }

        // GET: VenteController
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            List<Document> documentList = documentBusiness.getAllVentes();
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View(documentList);

        }
        [VerifyUserAttribute]
        public ActionResult Vente()
        {
            VenteModel model = new VenteModel();
            model.utilisateur = GetChefFromCookie();
            model.ClientList = clientBusiness.getClients();
            model.ProduitList = produitBusiness.getProduits();

            return View(model);
        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult facture(string totalFacture, string numDocument, int clientId)
        {
            int idFacture = achatBusiness.saveVente(totalFacture, numDocument, clientId);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Nouvelle vente: " + numDocument);
            return Json(idFacture);


        }
        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult factureDetails(String total, int idFacture, int idProduit, int quantite)
        {
            achatBusiness.saveDetailsVente(total, idFacture, idProduit, quantite);
            return Json("true");


        }


        // GET: AchatController/Details/5
        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            FactureModel model = new FactureModel();
            model.utilisateur = GetChefFromCookie();
            model.document = documentBusiness.getVenteById(id);
            model.achatList = achatBusiness.getAchatByDocumentId(id);
            return View(model);
        }

        // Partial View 
        [VerifyUserAttribute]
        public ActionResult AchatProduit(int idProduit)
        {
            AchatProduitModel model = new AchatProduitModel();
            model.produit = produitBusiness.getProduitById(idProduit);
            model.stock = stockBusiness.getStockByProduitId(idProduit);
            return PartialView(model);
        }

        // POST: AchatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
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

        // GET: AchatController/Edit/5
        [VerifyUserAttribute]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AchatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
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



        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult SupprimerDocument(int idDocument)
        {


            documentBusiness.DeleteDocumentById(idDocument);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression vente");

            return Json("true");




        }

        // POST: AchatController/Delete/5
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

                if (operateur == null && magasinier == null && admin == null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }
    }
}