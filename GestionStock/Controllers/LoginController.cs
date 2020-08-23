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
    public class LoginController : Controller
    {
        static Utilisateur utilisateur = new Utilisateur();
        private IHostEnvironment _env;

        public LoginController(IHostEnvironment env)
        {
            _env = env;

        }


        [VerifyUserAttribute]
        public IActionResult Index()
        {
            ViewBag.utilisateur = utilisateur.nom ;
            return View();
        }

        [VerifyUserAttribute]
        public IActionResult Help()
        {

            //ViewBag.utilisateur = utilisateur.nomUtilisateur ;
            return View();
        }

        [HttpPost]
        [VerifyUserAttribute]

        public JsonResult LoginCheck(String username, String password)
        {
            UtilisateurBusiness userBusisness = new UtilisateurBusinessImp();
            Utilisateur util = userBusisness.connecterUtilisateur(username, password);


            if (util == null)
                
                return Json("false");

            if (util != null)
            {
                Log.logWriter(_env, util);
                HttpContext.Session.SetString(util.fonction, JsonConvert.SerializeObject(util));
                return Json("true");
            }
           
            return null;


        }

     
        public ActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index","Login");
        }


        //----------------------------------------------------------------------

        public class VerifyUserAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var operateur = filterContext.HttpContext.Session.GetString("operateur");
                var magasinier = filterContext.HttpContext.Session.GetString("magasinier");
                var admin = filterContext.HttpContext.Session.GetString("administrateur");

                if (operateur != null || magasinier != null || admin != null)
                    filterContext.Result = new RedirectResult(string.Format("/home"));
            }
        }




    }
}
