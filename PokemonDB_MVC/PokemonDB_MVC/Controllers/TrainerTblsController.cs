using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokemonDB_MVC.Models;

namespace PokemonDB_MVC.Controllers
{
    [RoutePrefix("hall-of-fame")]
    public class TrainerTblsController : Controller
    {
        private PokemonDBEntitiesConnectionString db = new PokemonDBEntitiesConnectionString();

        // GET: TrainerTbls
        [Route("list", Name = "listpage")]
        public ActionResult Index()
        {
            var trainerTbls = db.TrainerTbls.Include(t => t.PokemonTbl);
            return View(trainerTbls.ToList());
        }

        [Route("details")]
        // GET: TrainerTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("Error - data does not exist for this Trainer");
            }
            TrainerTbl trainerTbl = db.TrainerTbls.Find(id);
            if (trainerTbl == null)
            {
                throw new Exception("Error - data does not exist for this Trainer");
            }
            return View(trainerTbl);
        }

        [Route("create")]
        // GET: TrainerTbls/Create
        public ActionResult Create()
        {
            ViewBag.PokedexNo = new SelectList(db.PokemonTbls, "PokedexNo", "PokemonName");
            return View();
        }

        [Route("create")]
        // POST: TrainerTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerID,TrainerName,Gender,PokedexNo")] TrainerTbl trainerTbl)
        {
            if (ModelState.IsValid)
            {
                db.TrainerTbls.Add(trainerTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PokedexNo = new SelectList(db.PokemonTbls, "PokedexNo", "PokemonName", trainerTbl.PokedexNo);
            return View(trainerTbl);
        }

        [Route("edit")]
        // GET: TrainerTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("Error - data does not exist for this Trainer");
            }
            TrainerTbl trainerTbl = db.TrainerTbls.Find(id);
            if (trainerTbl == null)
            {
                throw new Exception("Error - data does not exist for this Trainer");
            }
            ViewBag.PokedexNo = new SelectList(db.PokemonTbls, "PokedexNo", "PokemonName", trainerTbl.PokedexNo);
            return View(trainerTbl);
        }

        [Route("edit")]
        // POST: TrainerTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerID,TrainerName,Gender,PokedexNo")] TrainerTbl trainerTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainerTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PokedexNo = new SelectList(db.PokemonTbls, "PokedexNo", "PokemonName", trainerTbl.PokedexNo);
            return View(trainerTbl);
        }

        [Route("delete")]
        // GET: TrainerTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("Error - data does not exist for this Trainer");
            }
            TrainerTbl trainerTbl = db.TrainerTbls.Find(id);
            if (trainerTbl == null)
            {
                throw new Exception("Error - data does not exist for this Trainer");
            }
            return View(trainerTbl);
        }

        [Route("delete")]
        // POST: TrainerTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerTbl trainerTbl = db.TrainerTbls.Find(id);
            db.TrainerTbls.Remove(trainerTbl);
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
