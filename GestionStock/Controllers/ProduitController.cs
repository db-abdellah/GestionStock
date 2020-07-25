using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
      
        private IHostEnvironment _env;

       
        public ProduitController(IHostEnvironment env)
        {
            _env = env;

        }
        // GET: ProduitController
        public ActionResult Index()
        {
            ProduitsModel model = new ProduitsModel();
            model.produits = produitBusiness.getProduits();
            model.utilisateur = GetChefFromCookie();
            return View(model);
        }

        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            ProduitModel model = new ProduitModel();
            model.utilisateur = GetChefFromCookie();
            model.produit = produitBusiness.getProduitById(id);
            return View(model);
        }

        // GET: ProduitController/Create
        public ActionResult Create()
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View();
        }

        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit, IFormFile file)
        {
            
                Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                int idProduit = produitBusiness.saveProduit(produit);
                if(file.Length>0)
            {

                var dir = _env.ContentRootPath + @"/images/Produits";

                using (var filestream = new FileStream(Path.Combine(dir, idProduit +".jpeg"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(filestream);

                }
            }
                return RedirectToAction(nameof(Index));
            
        }

        // GET: ProduitController/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Produit produit = produitBusiness.getProduitById(id);
            return View(produit);
        }

        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produit produit, IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {

                    var dir = _env.ContentRootPath + @"/images/Produits";

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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProduitController/Delete/5
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

        [HttpPost]
        public JsonResult SupprimerProduit(int idProduit)
        {


            produitBusiness.DeleteProduitById(idProduit);
            return Json("true");




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
