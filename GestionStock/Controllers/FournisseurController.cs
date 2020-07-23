using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStock.Models.Business;
using GestionStock.Models.Business.Imp;
using GestionStock.Models.Entities;
using GestionStock.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace GestionStock.Controllers
{
    public class FournisseurController : Controller
    {
        private FournisseurBusiness fournisseurBusiness = new FournisseurBusinessImp();

        // GET: FournisseurController
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            FournisseursModel fournisseurs = new FournisseursModel();
            fournisseurs.fournisseurs = fournisseurBusiness.getFournisseurs();
            fournisseurs.utilisateur = GetChefFromCookie();

            return View(fournisseurs);
        }

        
        // GET: FournisseurController/Create
        public ActionResult Create()
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View();
        }

        // POST: FournisseurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fournisseur fournisseur)
        {
            try
            {
                fournisseurBusiness.saveFournisseur(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            FournisseurModel fournisseur = new FournisseurModel();
             fournisseur.utilisateur = GetChefFromCookie();
            fournisseur.fournisseur = fournisseurBusiness.getFournisseurById(id);
            return View(fournisseur);
        }

        // GET: FournisseurController/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Fournisseur fournisseur = fournisseurBusiness.getFournisseurById(id);
            return View(fournisseur);
        }

        // POST: FournisseurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Fournisseur fournisseur)
        {
            try
            {
                Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                fournisseurBusiness.updateFournisseur(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FournisseurController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FournisseurController/Delete/5
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
