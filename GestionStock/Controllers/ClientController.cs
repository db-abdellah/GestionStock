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
    public class ClientController : Controller
    {
        private ClientBusiness clientBusiness = new ClientBusinessImp();

        // GET: FournisseurController
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            ClientsModel clients = new ClientsModel();
            clients.clients = clientBusiness.getClients();
            clients.utilisateur = GetChefFromCookie();

            return View(clients);
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
        public ActionResult Create(Client client)
        {
            try
            {
                clientBusiness.saveClient(client);
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

        // GET: FournisseurController/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Client client = clientBusiness.getClientById(id);
            return View(client);
        }

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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult SupprimerClient(int idClient)
        {


            clientBusiness.DeleteClientById(idClient);
            return Json("true");




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
