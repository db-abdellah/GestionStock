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
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            ESModel model = produitBusiness.getProduitsAndStock();
            model.utilisateur = GetChefFromCookie();

            return View(model);
        }
        [VerifyUserAttribute]
        public ActionResult sortie()
        {

            ESModel model = produitBusiness.getProduitsAndAtelierStock();
            model.utilisateur = GetChefFromCookie();

            return View(model);
        }

        [VerifyUserAttribute]
        // GET: EntreeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [VerifyUserAttribute]
        // GET: EntreeController/Create
        public ActionResult Create()
        {
            return View();
        }

        [VerifyUserAttribute]
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

        [VerifyUserAttribute]
        // GET: EntreeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntreeController/Edit/5
        [VerifyUserAttribute]
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
        [VerifyUserAttribute]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntreeController/Delete/5
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
        public JsonResult EntreeAtelier(int idProduit,int qte)
        {


            entreeBusiness.EntreeAtelier(idProduit,qte);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Entrée produit id:"+idProduit+"   Qté: "+qte);
            return Json("true");




        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult SortieAtelier(int idProduit, int qte)
        {


            entreeBusiness.SortieAtelier(idProduit, qte);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Sortie produit id:" + idProduit + "  Qté: " + qte);
            return Json("true");




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

                if (operateur == null && magasinier == null && admin==null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }
    }
}
