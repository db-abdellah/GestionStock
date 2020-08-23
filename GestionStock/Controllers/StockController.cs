using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStock.Handlers;
using GestionStock.Models.Business;
using GestionStock.Models.Business.Imp;
using GestionStock.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GestionStock.Controllers
{
    public class StockController : Controller
    {
        StockBusiness stockBusiness = new StockBusinessImp();

        private IHostEnvironment _env;


        public StockController(IHostEnvironment env)
        {
            _env = env;

        }
        // GET: StockController
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            
            List<Stock> listStock = stockBusiness.getAllStock();
            return View(listStock);
        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult UpdateQteReel( int idStock,  int quantite)
        {
            stockBusiness.UpdateQteReel( idStock, quantite);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Mise à jour stock réel : " + idStock);
            return Json("true");


        }

        // GET: StockController/Details/5
        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockController/Create
        [VerifyUserAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockController/Create
        [VerifyUserAttribute]
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

        // GET: StockController/Edit/5
        [VerifyUserAttribute]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StockController/Edit/5
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

        // GET: StockController/Delete/5
        [VerifyUserAttribute]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StockController/Delete/5
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
