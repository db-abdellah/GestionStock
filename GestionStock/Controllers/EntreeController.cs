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
    public class EntreeController : Controller
    {
        private static ProduitBusiness produitBusiness = new ProduitBusinessImp();
        private static EntreeBusiness entreeBusiness = new EntreeBusinessImp();
        private IHostEnvironment _env;

        public EntreeController(IHostEnvironment env)
        {
            _env = env;

        }
        // GET: EntreeController
        public ActionResult Index()
        {
            ESModel model = produitBusiness.getProduitsAndStock();
            model.utilisateur = GetChefFromCookie();

            return View(model);
        }
        public ActionResult sortie()
        {

            ESModel model = produitBusiness.getProduitsAndAtelierStock();
            model.utilisateur = GetChefFromCookie();

            return View(model);
        }

        // GET: EntreeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntreeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntreeController/Create
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

        // GET: EntreeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntreeController/Edit/5
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

        // GET: EntreeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntreeController/Delete/5
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
        public JsonResult EntreeAtelier(int idProduit,int qte)
        {


            entreeBusiness.EntreeAtelier(idProduit,qte);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Entrée produit :"+idProduit+" Qté: "+qte);
            return Json("true");




        }

        [HttpPost]
        public JsonResult SortieAtelier(int idProduit, int qte)
        {


            entreeBusiness.SortieAtelier(idProduit, qte);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Sortie produit :" + idProduit + "Qté: " + qte);
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
