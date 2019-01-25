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
    public class KitobxonController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Kitobxon/
        public ActionResult Index()
        {
            return View(db.Kitobxons.ToList());
        }

        // GET: /Kitobxon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitobxon kitobxon = db.Kitobxons.Find(id);
            if (kitobxon == null)
            {
                return HttpNotFound();
            }
            return View(kitobxon);
        }

        // GET: /Kitobxon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Kitobxon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="KitobxonId,familiya,ismi,sharifi,tugilgan_yili,millati,passport_seriya,passport_nomer,adress,telefon")] Kitobxon kitobxon)
        {
            if (ModelState.IsValid)
            {
                db.Kitobxons.Add(kitobxon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kitobxon);
        }

        // GET: /Kitobxon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitobxon kitobxon = db.Kitobxons.Find(id);
            if (kitobxon == null)
            {
                return HttpNotFound();
            }
            return View(kitobxon);
        }

        // POST: /Kitobxon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="KitobxonId,familiya,ismi,sharifi,tugilgan_yili,millati,passport_seriya,passport_nomer,adress,telefon")] Kitobxon kitobxon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitobxon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kitobxon);
        }

        // GET: /Kitobxon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitobxon kitobxon = db.Kitobxons.Find(id);
            if (kitobxon == null)
            {
                return HttpNotFound();
            }
            return View(kitobxon);
        }

        // POST: /Kitobxon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitobxon kitobxon = db.Kitobxons.Find(id);
            db.Kitobxons.Remove(kitobxon);
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
