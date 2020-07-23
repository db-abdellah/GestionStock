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
    public class AchatController : Controller
    {
        private static FournisseurBusiness fournisseurBusiness = new FournisseurBusinessImp();
        private static ProduitBusiness produitBusiness= new ProduitBusinessImp();
        // GET: AchatController
        public ActionResult Index()
        {
            AchatModel model = new AchatModel();
            model.utilisateur = GetChefFromCookie();
            model.fournisseurList = fournisseurBusiness.getFournisseurs();
            model.ProduitList = produitBusiness.getProduits();

            return View(model);
        }
        public ActionResult Achat()
        {
            AchatModel model = new AchatModel();
            model.utilisateur = GetChefFromCookie();
            model.fournisseurList = fournisseurBusiness.getFournisseurs();
            model.ProduitList = produitBusiness.getProduits();

            return View(model);
        }

        // GET: AchatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AchatController/Create
        public ActionResult Create()
        {
            return View();
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

        // GET: AchatController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
