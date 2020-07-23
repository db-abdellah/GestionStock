using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStock.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace GestionStock.Controllers
{
    public class HomeController : Controller
    {
        [VerifyUserAttribute]
        public IActionResult Index()
        {
            return View(GetChefFromCookie());
        }





        //--------------------------------------------------------------------------------------

        [VerifyUserAttribute]
        private Utilisateur GetChefFromCookie()
        {
            var jsonResult = HttpContext.Session.GetString("Utilisateur");

            return JsonConvert.DeserializeObject<Utilisateur>(jsonResult);
        }


        //---------------------------------------------------------------------------------------

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
