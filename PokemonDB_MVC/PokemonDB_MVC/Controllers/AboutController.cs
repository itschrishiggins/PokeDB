using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokemonDB_MVC.Controllers
{
    [RoutePrefix("about")]
    public class AboutController : Controller
    {
        // GET: About
        [Route("about", Name = "aboutpage")]
        public ActionResult About()
        {
            return View();
        }
    }
}