using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using System.IO;

namespace Library.Controllers
{
    public class KitobController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Kitob/
        public ActionResult Index()
        {
            var kitobs = db.Kitobs.Include(k => k.janr);
            return View(kitobs.ToList());
        }
        public ActionResult Yangilari()
        {
            var kitobs = from a in db.Kitobs.Include(k => k.janr) orderby a.chop_etilg_yil descending select a;
            return View(kitobs.ToList());
        }
       
        public ActionResult GetPoisk()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostPoisk()
        {
            string avtor = Request.Form["avtor"].ToString(), nomi = Request.Form["nomi"].ToString(), nash = Request.Form["nashriyot"].ToString(),
                janr = Request.Form["janr"].ToString();
            var kitoblar = db.Kitobs.Include(a => a.janr);
            var natija = from a in kitoblar.ToList<Kitob>()
                           where
                               (string.IsNullOrEmpty(avtor) || a.avtor.ToLower().Contains(avtor.ToLower())) &&
                               (string.IsNullOrEmpty(nomi) || a.nomi.ToLower().Contains(nomi.ToLower())) &&
                               (string.IsNullOrEmpty(nash) || a.nashriyot.ToLower().Contains(nash.ToLower())) &&
                               (string.IsNullOrEmpty(janr) || a.janr.janr_nomi.ToLower().Contains(janr.ToLower()))                            
                           select a;
            return View(natija.ToList());
        }
      
        public ActionResult Avtorlar()
        {
            return View(db.Kitobs.ToList());
        }
        public ActionResult Avtor_boyicha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string avtor = db.Kitobs.Find(id).avtor;
            if (avtor == null)
            {
                return HttpNotFound();
            }
            var nat = from a in db.Kitobs.Include(b => b.janr) where a.avtor == avtor select a;
            return View(nat.ToList());
        }
        // GET: /Kitob/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitob kitob = db.Kitobs.Find(id);
            if (kitob == null)
            {
                return HttpNotFound();
            }
            return View(kitob);
        }

        // GET: /Kitob/Create
        public ActionResult Create()
        {
            ViewBag.JanrId = new SelectList(db.Janrs, "JanrId", "janr_nomi");
            return View();
        }

        // POST: /Kitob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="KitobId,nomi,chop_etilg_yil,avtor,JanrId,nashriyot,betlar_soni,soni,qisqacha_malumot")] Kitob kitob,HttpPostedFileBase f)
        {
            if (f != null && f.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Rasmlar"), Path.GetFileName(f.FileName));
                f.SaveAs(path);
                kitob.rasmi = "/Rasmlar/" + Path.GetFileName(f.FileName);
            }
            if (ModelState.IsValid)
            {
                db.Kitobs.Add(kitob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JanrId = new SelectList(db.Janrs, "JanrId", "janr_nomi", kitob.JanrId);
            return View(kitob);
        }

        // GET: /Kitob/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitob kitob = db.Kitobs.Find(id);
            if (kitob == null)
            {
                return HttpNotFound();
            }
            ViewBag.JanrId = new SelectList(db.Janrs, "JanrId", "janr_nomi", kitob.JanrId);
            return View(kitob);
        }

        // POST: /Kitob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="KitobId,nomi,chop_etilg_yil,avtor,JanrId,nashriyot,betlar_soni,soni,rasmi,qisqacha_malumot")] Kitob kitob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JanrId = new SelectList(db.Janrs, "JanrId", "janr_nomi", kitob.JanrId);
            return View(kitob);
        }

        // GET: /Kitob/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kitob kitob = db.Kitobs.Find(id);
            if (kitob == null)
            {
                return HttpNotFound();
            }
            return View(kitob);
        }

        // POST: /Kitob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kitob kitob = db.Kitobs.Find(id);
            db.Kitobs.Remove(kitob);
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
