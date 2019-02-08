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
using Fakultet.ViewModels;
using PagedList;

namespace Fakultet.Controllers
{
    public class StudentController : Controller
    {
        private FakultetContext db = new FakultetContext();

        // GET: Student
        public ActionResult Index(string textPretrage, int? strana)
        {
            var studenti = from s in db.Student select s;

            if(!string.IsNullOrEmpty(textPretrage))
            {
                studenti = studenti.Where(s => s.Ime.Contains(textPretrage) || s.Prezime.Contains(textPretrage));
            }

            return View(studenti.OrderBy(s => s.Ime).ToPagedList(strana ?? 1, 3));
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JMBG, Ime, Prezime, Godine")] Student student, string[] izabraniPredmeti)
        {
            if(izabraniPredmeti != null)
            {
                student.Predmeti = new List<Predmet>();
                foreach(var predmet in izabraniPredmeti)
                {
                    student.Predmeti.Add(db.Predmet.Find(predmet));
                }
            }

            if (ModelState.IsValid)
            {
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            var sviPredmeti = db.Predmet;
            var listaPredmeta = new List<DodeljeniPredmet>();
            foreach (var predmet in sviPredmeti)
            {
                listaPredmeta.Add(new DodeljeniPredmet
                {
                    Naziv = predmet.Naziv,
                    Dodeljen = student.Predmeti.Contains(predmet)
                });
            }
            ViewBag.Predmeti = listaPredmeta;

            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string JMBG, string[] izabraniPredmeti)
        {
            if(JMBG == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(JMBG);

            if(student == null)
            {
                return HttpNotFound();
            }

            if (TryUpdateModel(student, "", new string[] { "Ime", "Prezime", "Godine" }))
            {
                foreach (var predmet in db.Predmet)
                {
                    if (izabraniPredmeti.Contains(predmet.Naziv))
                    {
                        if (!student.Predmeti.Contains(predmet))
                        {
                            student.Predmeti.Add(predmet);
                        }
                    }
                    else
                    {
                        if (student.Predmeti.Contains(predmet))
                        {
                            student.Predmeti.Remove(predmet);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View(student);
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Student.Find(id);
            db.Student.Remove(student);
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
