using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokemonDB_MVC.Models;
using System.Net.Http;

namespace PokemonDB_MVC.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        private PokemonDBEntitiesConnectionString db = new PokemonDBEntitiesConnectionString();

        // GET: Home
        [Route("home", Name ="homepage")]
        public ActionResult Home()
        {
            return View(db.PokemonTbls.ToList());
        }
    }
}