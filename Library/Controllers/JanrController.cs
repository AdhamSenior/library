using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class JanrController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Janr/
        public ActionResult Index()
        {
            return View(db.Janrs.ToList());
        }

        // GET: /Janr/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Janr janr = db.Janrs.Find(id);
            if (janr == null)
            {
                return HttpNotFound();
            }
            return View(janr);
        }
       
        // GET: /Janr/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Janr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="JanrId,janr_nomi")] Janr janr)
        {
            if (ModelState.IsValid)
            {
                db.Janrs.Add(janr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(janr);
        }

        // GET: /Janr/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Janr janr = db.Janrs.Find(id);
            if (janr == null)
            {
                return HttpNotFound();
            }
            return View(janr);
        }

        // POST: /Janr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="JanrId,janr_nomi")] Janr janr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(janr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(janr);
        }

        // GET: /Janr/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Janr janr = db.Janrs.Find(id);
            if (janr == null)
            {
                return HttpNotFound();
            }
            return View(janr);
        }

        // POST: /Janr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Janr janr = db.Janrs.Find(id);
            db.Janrs.Remove(janr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Janrlar()
        {
            return View(db.Janrs.ToList());
        }
        public ActionResult Janr_boyicha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string janr = db.Janrs.Find(id).janr_nomi;
            if (janr == null)
            {
                return HttpNotFound();
            }
            var nat = from a in db.Kitobs.Include(b => b.janr) where a.janr.janr_nomi == janr select a;
            return View(nat.ToList());
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
