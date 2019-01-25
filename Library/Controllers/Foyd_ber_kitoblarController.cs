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
    public class Foyd_ber_kitoblarController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Foyd_ber_kitoblar/
        public ActionResult Index()
        {
            var foyd_ber_kitoblars = db.Foyd_ber_kitoblars.Include(f => f.kitob).Include(f => f.kitobxon).Include(f => f.xodim);
            return View(foyd_ber_kitoblars.ToList());
        }
        public ActionResult Protsent()
        {
            float s1 = 0;
            foreach(Kitob a in db.Kitobs.Include(a=>a.janr))
            {
                s1 += a.soni;
            }
            s1=db.Foyd_ber_kitoblars.Count()/s1;
            ViewBag.s1 = s1;
            return View();
        }
        // GET: /Foyd_ber_kitoblar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foyd_ber_kitoblar foyd_ber_kitoblar = db.Foyd_ber_kitoblars.Find(id);
            if (foyd_ber_kitoblar == null)
            {
                return HttpNotFound();
            }
            return View(foyd_ber_kitoblar);
        }

        // GET: /Foyd_ber_kitoblar/Create
        public ActionResult Create()
        {
            ViewBag.KitobId = new SelectList(db.Kitobs, "KitobId", "nomi");
            ViewBag.KitobxonId = new SelectList(db.Kitobxons, "KitobxonId", "familiya");
            ViewBag.XodimId = new SelectList(db.Xodims, "XodimId", "familiya");
            return View();
        }

        // POST: /Foyd_ber_kitoblar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Foyd_ber_kitoblarId,XodimId,KitobxonId,KitobId,berilgan_vaqti,qaytarilish_vaqti")] Foyd_ber_kitoblar foyd_ber_kitoblar)
        {
            if (ModelState.IsValid)
            {
                db.Foyd_ber_kitoblars.Add(foyd_ber_kitoblar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KitobId = new SelectList(db.Kitobs, "KitobId", "nomi", foyd_ber_kitoblar.KitobId);
            ViewBag.KitobxonId = new SelectList(db.Kitobxons, "KitobxonId", "familiya", foyd_ber_kitoblar.KitobxonId);
            ViewBag.XodimId = new SelectList(db.Xodims, "XodimId", "familiya", foyd_ber_kitoblar.XodimId);
            return View(foyd_ber_kitoblar);
        }

        // GET: /Foyd_ber_kitoblar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foyd_ber_kitoblar foyd_ber_kitoblar = db.Foyd_ber_kitoblars.Find(id);
            if (foyd_ber_kitoblar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KitobId = new SelectList(db.Kitobs, "KitobId", "nomi", foyd_ber_kitoblar.KitobId);
            ViewBag.KitobxonId = new SelectList(db.Kitobxons, "KitobxonId", "familiya", foyd_ber_kitoblar.KitobxonId);
            ViewBag.XodimId = new SelectList(db.Xodims, "XodimId", "familiya", foyd_ber_kitoblar.XodimId);
            return View(foyd_ber_kitoblar);
        }

        // POST: /Foyd_ber_kitoblar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Foyd_ber_kitoblarId,XodimId,KitobxonId,KitobId,berilgan_vaqti,qaytarilish_vaqti")] Foyd_ber_kitoblar foyd_ber_kitoblar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foyd_ber_kitoblar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KitobId = new SelectList(db.Kitobs, "KitobId", "nomi", foyd_ber_kitoblar.KitobId);
            ViewBag.KitobxonId = new SelectList(db.Kitobxons, "KitobxonId", "familiya", foyd_ber_kitoblar.KitobxonId);
            ViewBag.XodimId = new SelectList(db.Xodims, "XodimId", "familiya", foyd_ber_kitoblar.XodimId);
            return View(foyd_ber_kitoblar);
        }

        // GET: /Foyd_ber_kitoblar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foyd_ber_kitoblar foyd_ber_kitoblar = db.Foyd_ber_kitoblars.Find(id);
            if (foyd_ber_kitoblar == null)
            {
                return HttpNotFound();
            }
            return View(foyd_ber_kitoblar);
        }

        // POST: /Foyd_ber_kitoblar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Foyd_ber_kitoblar foyd_ber_kitoblar = db.Foyd_ber_kitoblars.Find(id);
            db.Foyd_ber_kitoblars.Remove(foyd_ber_kitoblar);
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
