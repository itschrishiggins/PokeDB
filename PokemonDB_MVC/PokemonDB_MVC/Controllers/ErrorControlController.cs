using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonDB_MVC.Controllers
{
    public class ErrorControlController : Controller
    {
        // GET: ErrorControl
        public ActionResult Http400()
        {
            Response.StatusCode = 400;
            return View();
        }

        public ActionResult Http404()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}