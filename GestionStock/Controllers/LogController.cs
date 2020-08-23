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
    public class LogController : Controller
    {

        static Utilisateur utilisateur = new Utilisateur();
        private IHostEnvironment _env;

        public LogController(IHostEnvironment env)
        {
            _env = env;

        }
        public IActionResult Index()
        {
            LogModel model = new LogModel();
            model.util= GetChefFromCookie();
            model.logList = Log.fileToList(_env);
            
            return View(model);
        }

        public IActionResult Transactions()
        {
            TransactionListModel model = new TransactionListModel();
            model.util = GetChefFromCookie();
            model.list = Log.fileToListTransactions(_env);

            return View(model);
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
