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
    public class UtilisateurController : Controller
    {

        private UtilisateurBusiness UtilisateurBusiness = new UtilisateurBusinessImp();
        // GET: UtilisateurController
        public ActionResult Index()
        {
            UtilisateursModel model = new UtilisateursModel();
            model.utilisateurList = UtilisateurBusiness.getUtilisateurs();
            model.utilisateur = GetChefFromCookie();
            return View(model);
        }

      
        // GET: UtilisateurController/Create
        public ActionResult Create()
        {

            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View();
        }

        // POST: UtilisateurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Utilisateur utilisateur)
        {
            try
            {
                Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                UtilisateurBusiness.saveUtilisateur(utilisateur);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UtilisateurController/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Utilisateur utilisateur = UtilisateurBusiness.getUtilisateurById(id);
            return View(utilisateur);
        }

        // POST: UtilisateurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Utilisateur utilisateur)
        {
          
                Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                UtilisateurBusiness.updateUtilisateur(utilisateur);
                return RedirectToAction(nameof(Index));
            
        }

        // GET: UtilisateurController/Delete/5
        public ActionResult Delete(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Utilisateur utilisateur = UtilisateurBusiness.getUtilisateurById(id);
            
            return View(utilisateur);
            
        }

        // POST: UtilisateurController/Delete/5
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

        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            UtilisateurModel model = new UtilisateurModel();
            model.utilisateur = UtilisateurBusiness.getUtilisateurById(id);
            model.Chef = GetChefFromCookie();
            return View(model);
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
