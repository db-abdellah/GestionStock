using System.Collections.Generic;
using System.IO;
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
    public class UtilisateurController : Controller
    {

        private static UtilisateurBusiness utilisateurBusiness = new UtilisateurBusinessImp();

        private IHostEnvironment _env;

        public UtilisateurController(IHostEnvironment env)
        {
            _env = env;

        }
        // GET: UtilisateurController
        public ActionResult Index()
        {
            UtilisateursModel model = new UtilisateursModel();
            model.utilisateurList = utilisateurBusiness.getUtilisateurs();
            model.utilisateur = GetChefFromCookie();
            return View(model);
        }

        [HttpPost]
        public JsonResult loginCheck(string login)
        {
            int check= utilisateurBusiness.checkNewLogin(login);
            if (check==1)

                return Json("true");
            else
                return Json("false");
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
        public ActionResult Create(Utilisateur utilisateur , IFormFile file)
        {
              Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                int idUtilisateur= utilisateurBusiness.saveUtilisateur(utilisateur);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Nouveau utlisateur : " + utilisateur.nom + utilisateur.prenom);
            if (file != null)
            {

                var dir = _env.ContentRootPath + @"/wwwroot/images/Utilisateur";

                using (var filestream = new FileStream(Path.Combine(dir, idUtilisateur + ".jpeg"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(filestream);

                }
            }
                    return RedirectToAction(nameof(Index));
          
        }

        // GET: UtilisateurController/Edit/5
        public ActionResult Edit(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Utilisateur utilisateur = utilisateurBusiness.getUtilisateurById(id);
            return View(utilisateur);
        }

        // POST: UtilisateurController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Utilisateur utilisateur, IFormFile file)
        {

            if (file != null)
            {

                var dir = _env.ContentRootPath + @"/wwwroot/images/Utilisateur";

                using (var filestream = new FileStream(Path.Combine(dir, utilisateur.id + ".jpeg"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(filestream);

                }
            }
            Utilisateur util = GetChefFromCookie();
                ViewBag.utilisateur = util;
                utilisateurBusiness.updateUtilisateur(utilisateur);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Mise à jour d'utilisateur : " + utilisateur.id);
            return RedirectToAction(nameof(Index));
            
        }

        

        // GET: UtilisateurController/Delete/5
        public ActionResult Delete(int id)
        {
            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            Utilisateur utilisateur = utilisateurBusiness.getUtilisateurById(id);
            
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
            model.utilisateur = utilisateurBusiness.getUtilisateurById(id);
            model.Chef = GetChefFromCookie();
            return View(model);
        }

        [HttpPost]
        public JsonResult SupprimerUtil(int idUtil)
        {


            utilisateurBusiness.DeleteUserById(idUtil);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression d'utilisateur : " + idUtil);
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
