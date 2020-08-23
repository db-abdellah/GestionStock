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
        public ActionResult Index()
        {
            List<Document> documentList = documentBusiness.getAllVentes();
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View(documentList);
           
        }
        public ActionResult Vente()
        {
            VenteModel model = new VenteModel();
            model.utilisateur = GetChefFromCookie();
            model.ClientList = clientBusiness.getClients();
            model.ProduitList = produitBusiness.getProduits();

            return View(model);
        }

        [HttpPost]
        public JsonResult facture(string totalFacture, string numDocument, int clientId)
        {
            int idFacture = achatBusiness.saveVente(totalFacture, numDocument, clientId);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Nouvelle vente: " + numDocument);
            return Json(idFacture);


        }
        [HttpPost]
        public JsonResult factureDetails(String total, int idFacture, int idProduit, int quantite)
        {
            achatBusiness.saveDetailsVente(total, idFacture, idProduit, quantite);
            return Json("true");


        }


        // GET: AchatController/Details/5
        public ActionResult Details(int id)
        {
            FactureModel model = new FactureModel();
            model.utilisateur = GetChefFromCookie();
            model.document = documentBusiness.getDocumentById(id);
            model.achatList = achatBusiness.getAchatByDocumentId(id);
            return View(model);
        }

        // Partial View 
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
        public ActionResult Edit(int id)
        {
            return View();
        }

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



        [HttpPost]
        public JsonResult SupprimerDocument(int idDocument)
        {


            documentBusiness.DeleteDocumentById(idDocument);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression vente");

            return Json("true");




        }

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
            var jsonResult = HttpContext.Session.GetString("Utilisateur");

            return JsonConvert.DeserializeObject<Utilisateur>(jsonResult);
        }
        //----------------------------------------------------------------------

        public class VerifyUserAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var user = filterContext.HttpContext.Session.GetString("Utilisateur");
                if (user == null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }
    }
}
