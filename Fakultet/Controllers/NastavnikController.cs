using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fakultet.DatabaseContext;
using Fakultet.Models;
using PagedList;

namespace Fakultet.Controllers
{
    public class NastavnikController : Controller
    {
        private FakultetContext db = new FakultetContext();

        // GET: Nastavnik
        public ActionResult Index(string textPretrage, int? strana)
        {
            var nastavnici = from n in db.Nastavnik select n;

            if(!string.IsNullOrEmpty(textPretrage))
            {
                nastavnici = nastavnici.Where(n => n.Ime.Contains(textPretrage) || n.Prezime.Contains(textPretrage));
            }

            return View(nastavnici.OrderBy(n => n.Ime).ToPagedList(strana ?? 1, 3));
        }

        // GET: Nastavnik/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nastavnik nastavnik = db.Nastavnik.Find(id);
            if (nastavnik == null)
            {
                return HttpNotFound();
            }
            return View(nastavnik);
        }

        // GET: Nastavnik/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nastavnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JMBG,Ime,Prezime,DatumZaposljavanja")] Nastavnik nastavnik, string[] izabraniPredmeti)
        {
            if(izabraniPredmeti != null)
            {
                nastavnik.Predmeti = new List<Predmet>();
                foreach(var predmet in izabraniPredmeti)
                {
                    nastavnik.Predmeti.Add(db.Predmet.Find(predmet));
                }
            }

            if (ModelState.IsValid)
            {
                db.Nastavnik.Add(nastavnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nastavnik);
        }

        // GET: Nastavnik/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nastavnik nastavnik = db.Nastavnik.Find(id);
            if (nastavnik == null)
            {
                return HttpNotFound();
            }
            return View(nastavnik);
        }

        // POST: Nastavnik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JMBG,Ime,Prezime,DatumZaposljavanja")] Nastavnik nastavnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nastavnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nastavnik);
        }

        // GET: Nastavnik/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nastavnik nastavnik = db.Nastavnik.Find(id);
            if (nastavnik == null)
            {
                return HttpNotFound();
            }
            return View(nastavnik);
        }

        // POST: Nastavnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Nastavnik nastavnik = db.Nastavnik.Find(id);
            db.Nastavnik.Remove(nastavnik);
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
