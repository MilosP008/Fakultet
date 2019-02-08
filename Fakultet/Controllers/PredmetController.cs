using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fakultet.DatabaseContext;
using Fakultet.Models;
using PagedList;

namespace Fakultet.Controllers
{
    public class PredmetController : Controller
    {
        private FakultetContext db = new FakultetContext();

        // GET: Predmet
        public ActionResult Index(string textPretrage, int? strana)
        {
            var predmeti = db.Predmet.Include(p => p.Nastavnik);

            if(!string.IsNullOrEmpty(textPretrage))
            {
                predmeti = predmeti.Where(p => p.Naziv.Contains(textPretrage));
            }

            return View(predmeti.OrderBy(p => p.Naziv).ToPagedList(strana ?? 1, 3));
        }

        // GET: Predmet/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = db.Predmet.Find(id);
            if (predmet == null)
            {
                return HttpNotFound();
            }
            return View(predmet);
        }

        // GET: Predmet/Create
        public ActionResult Create()
        {
            List<Nastavnik> nastavnici = new List<Nastavnik>();
            foreach(var nastavnik in db.Nastavnik)
            {
                nastavnici.Add(new Nastavnik { JMBG = nastavnik.JMBG, Ime = nastavnik.Prezime + " " + nastavnik.Ime });
            }

            ViewBag.JMBGNastavnika = new SelectList(nastavnici, "JMBG", "Ime");
            return View();
        }

        // POST: Predmet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Naziv,Cena,JMBGNastavnika")] Predmet predmet)
        {
            if (ModelState.IsValid)
            {
                db.Predmet.Add(predmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Nastavnik> nastavnici = new List<Nastavnik>();
            foreach (var nastavnik in db.Nastavnik)
            {
                nastavnici.Add(new Nastavnik { JMBG = nastavnik.JMBG, Ime = nastavnik.Prezime + " " + nastavnik.Ime });
            }

            ViewBag.JMBGNastavnika = new SelectList(nastavnici, "JMBG", "Ime", predmet.JMBGNastavnika);
            return View(predmet);
        }

        // GET: Predmet/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = db.Predmet.Find(id);
            if (predmet == null)
            {
                return HttpNotFound();
            }

            List<Nastavnik> nastavnici = new List<Nastavnik>();
            foreach (var nastavnik in db.Nastavnik)
            {
                nastavnici.Add(new Nastavnik { JMBG = nastavnik.JMBG, Ime = nastavnik.Prezime + " " + nastavnik.Ime });
            }

            ViewBag.JMBGNastavnika = new SelectList(nastavnici, "JMBG", "Ime", predmet.JMBGNastavnika);
            return View(predmet);
        }

        // POST: Predmet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Naziv,Cena,JMBGNastavnika")] Predmet predmet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predmet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Nastavnik> nastavnici = new List<Nastavnik>();
            foreach (var nastavnik in db.Nastavnik)
            {
                nastavnici.Add(new Nastavnik { JMBG = nastavnik.JMBG, Ime = nastavnik.Prezime + " " + nastavnik.Ime });
            }

            ViewBag.JMBGNastavnika = new SelectList(nastavnici, "JMBG", "Ime", predmet.JMBGNastavnika);
            return View(predmet);
        }

        // GET: Predmet/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = db.Predmet.Find(id);
            if (predmet == null)
            {
                return HttpNotFound();
            }
            return View(predmet);
        }

        // POST: Predmet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Predmet predmet = db.Predmet.Find(id);
            db.Predmet.Remove(predmet);
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
