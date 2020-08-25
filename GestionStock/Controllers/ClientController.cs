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
    public class ClientController : Controller
    {
        private ClientBusiness clientBusiness = new ClientBusinessImp();
        private IHostEnvironment _env;

        public ClientController(IHostEnvironment env)
        {
            _env = env;

        }

        // GET: FournisseurController
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            ClientsModel clients = new ClientsModel();
            clients.clients = clientBusiness.getClients();
            clients.utilisateur = GetChefFromCookie();

            return View(clients);
        }

        [VerifyUserAttribute]
        // GET: FournisseurController/Create
        public ActionResult Create()
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View();
        }

        [VerifyUserAttribute]
        // POST: FournisseurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            try
            {
                clientBusiness.saveClient(client);

                Log.TransactionsWriter(_env, GetChefFromCookie(), "Creation du client");
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
            ClientModel client = new ClientModel();
            client.utilisateur = GetChefFromCookie();
            client.client = clientBusiness.getClientById(id);
            return View(client);
        }

        [VerifyUserAttribute]
        // GET: FournisseurController/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Client client = clientBusiness.getClientById(id);
            
            return View(client);
        }

        [VerifyUserAttribute]
        // POST: FournisseurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            try
            {
                Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                clientBusiness.updateClient(client);
                Log.TransactionsWriter(_env, GetChefFromCookie(), "Modification du client");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult SupprimerClient(int idClient)
        {


            clientBusiness.DeleteClientById(idClient);
            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression du client");
            return Json("true");




        }
        // POST: FournisseurController/Delete/5
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

                if ( magasinier == null && admin == null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }

    }
}
