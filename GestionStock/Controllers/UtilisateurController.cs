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
        [VerifyUserAttribute]
        public ActionResult Index()
        {
            UtilisateursModel model = new UtilisateursModel();
            model.utilisateurList = utilisateurBusiness.getUtilisateurs();
            model.utilisateur = GetChefFromCookie();
            return View(model);
        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult loginCheck(string login)
        {
            int check= utilisateurBusiness.checkNewLogin(login);
            if (check==1)

                return Json("true");
            else
                return Json("false");
        }

        // GET: UtilisateurController/Create
        [VerifyUserAttribute]
        public ActionResult Create()
        {

            Utilisateur util = GetChefFromCookie();
            ViewBag.utilisateur = util;
            return View();
        }

        // POST: UtilisateurController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [VerifyUserAttribute]
        public ActionResult Create(Utilisateur utilisateur , IFormFile file)
        {

            utilisateur = clearInput(utilisateur);
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
        [VerifyUserAttribute]
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
        [VerifyUserAttribute]
        public ActionResult Edit(Utilisateur utilisateur, IFormFile file)
        {
            utilisateur = clearInput(utilisateur);

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
        [VerifyUserAttribute]
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

        [VerifyUserAttribute]
        public ActionResult Details(int id)
        {
            UtilisateurModel model = new UtilisateurModel();
            model.utilisateur = utilisateurBusiness.getUtilisateurById(id);
            model.Chef = GetChefFromCookie();
            return View(model);
        }

        [HttpPost]
        [VerifyUserAttribute]
        public JsonResult SupprimerUtil(int idUtil)
        {


            utilisateurBusiness.DeleteUserById(idUtil);

            Log.TransactionsWriter(_env, GetChefFromCookie(), "Suppression d'utilisateur : " + idUtil);
            return Json("true");




        }


        private Utilisateur clearInput(Utilisateur util)
        {
            util.adresse = util.adresse.Replace("\'", " ");
            util.nom = util.nom.Replace("\'", " ");
            util.prenom = util.prenom.Replace("\'", " ");
            util.telephone = util.telephone.Replace("\'", " ");
            return util;
        }
        //----------------------------------------------------------------------
        [VerifyUserAttribute]
        private Utilisateur GetChefFromCookie()
        {
            var jsonResult = HttpContext.Session.GetString("administrateur");
            
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

                if ( admin == null)
                    filterContext.Result = new RedirectResult(string.Format("/Login"));
            }
        }
    }
}
