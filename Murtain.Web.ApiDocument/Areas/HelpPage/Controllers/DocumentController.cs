using Murtain.Web.ApiDocument.Areas.HelpPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace Murtain.Web.ApiDocument.Areas.HelpPage.Controllers
{
    public class DocumentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Readme() {
            return View();
        }
        public ActionResult Documentation()
        {
            return View();
        }
        public ActionResult Api()
        {
            return View();
        }
        public ActionResult Model()
        {
            return View();
        }
    }

}