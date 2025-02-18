using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokemonDB_MVC.Models;
using System.Net.Http;

namespace PokemonDB_MVC.Controllers
{
    //[HandleError]
    [RoutePrefix("pokedex")] 
    public class PokemonTblsController : Controller
    {
        private PokemonDBEntitiesConnectionString db = new PokemonDBEntitiesConnectionString();

        // GET: PokemonTbls

        [Route("pokedex", Name = "pokedexpage")]
        //[Route("{action = index}")]

        public ViewResult Index(string sortOrder, string searchType, string searchPokemon)
        {
            var SearchOptionList = new List<string>();
            SearchOptionList.Add("Normal");
            SearchOptionList.Add("Fire");
            SearchOptionList.Add("Water");
            SearchOptionList.Add("Grass");
            SearchOptionList.Add("Electric");
            SearchOptionList.Add("Ice");
            SearchOptionList.Add("Fighting");
            SearchOptionList.Add("Poison");
            SearchOptionList.Add("Ground");
            SearchOptionList.Add("Flying");
            SearchOptionList.Add("Psychic");
            SearchOptionList.Add("Bug");
            SearchOptionList.Add("Rock");
            SearchOptionList.Add("Ghost");
            SearchOptionList.Add("Dark");
            SearchOptionList.Add("Dragon");
            SearchOptionList.Add("Steel");
            SearchOptionList.Add("Fairy");

            ViewBag.searchType = new SelectList(SearchOptionList);

            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            var pokemon = from s in db.PokemonTbls
                          select s;

            if (!String.IsNullOrEmpty(searchType))
            {
                pokemon = pokemon.Where(s => s.Type1.Contains(searchType)
                                       || s.Type2.Contains(searchType));
            }

            if (!String.IsNullOrEmpty(searchPokemon))
            {
                pokemon = pokemon.Where(s => s.PokemonName.Contains(searchPokemon));

            }

            switch (sortOrder)
            {
                case "number_desc":
                    pokemon = pokemon.OrderByDescending(s => s.PokedexNo);
                    break;
                case "Name":
                    pokemon = pokemon.OrderBy(s => s.PokemonName);
                    break;
                case "name_desc":
                    pokemon = pokemon.OrderByDescending(s => s.PokemonName);
                    break;
                default:
                    pokemon = pokemon.OrderBy(s => s.PokedexNo);
                    break;
            }


            ModelState.Remove("searchType");
            ModelState.Remove("searchPokemon");
            return View(pokemon.ToList());
        }

        [HandleError]
        // GET: PokemonTbls/Details/5
        [Route("pokemon/{id?}")]

        public ActionResult Details(int? id)
        {
            if (id > 151)
            {
                throw new Exception("Error - data does not exist for this Pokémon");
            }

            if (id == null)
            {
                //throw new Exception("Error - data does not exist for this Pokémon"); 

                throw new Exception("Error - data does not exist for this Pokémon");
            }
            PokemonTbl pokemonTbl = db.PokemonTbls.Find(id);
            if (pokemonTbl == null)
            {
                throw new Exception("Error - data does not exist for this Pokémon");
            }
            PokemonInfo dataFromAPI = new PokemonInfo();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("LOCALADDRESS/api/"); // local address followed by api/ e.g: ("http://localhost:49189/api/"); 
            var apiControllerInfo = client.GetAsync("PokemonInfo/" + id); //comes from the API help and append id
            apiControllerInfo.Wait();

            var responseMessage = apiControllerInfo.Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var getInfoTask = responseMessage.Content.ReadAsAsync<PokemonInfo>();
                getInfoTask.Wait();
                dataFromAPI = getInfoTask.Result;            
                ViewBag.apiData = dataFromAPI;

            }
            else
            {
                ViewBag.apiData = "Error";
            }

            return View(pokemonTbl);
        }

        // GET: PokemonTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PokemonTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PokedexNo,PokemonName,Type1,Type2,Image")] PokemonTbl pokemonTbl)
        {
            if (ModelState.IsValid)
            {
                db.PokemonTbls.Add(pokemonTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pokemonTbl);
        }

        // GET: PokemonTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PokemonTbl pokemonTbl = db.PokemonTbls.Find(id);
            if (pokemonTbl == null)
            {
                return HttpNotFound();
            }
            return View(pokemonTbl);
        }

        // POST: PokemonTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PokedexNo,PokemonName,Type1,Type2,Image")] PokemonTbl pokemonTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pokemonTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pokemonTbl);
        }

        // GET: PokemonTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PokemonTbl pokemonTbl = db.PokemonTbls.Find(id);
            if (pokemonTbl == null)
            {
                return HttpNotFound();
            }
            return View(pokemonTbl);
        }

        // POST: PokemonTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PokemonTbl pokemonTbl = db.PokemonTbls.Find(id);
            db.PokemonTbls.Remove(pokemonTbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
