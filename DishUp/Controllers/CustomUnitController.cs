using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DishUp;

namespace DishUp.Controllers
{
    public class CustomUnitController : Controller
    {
        private dishupEntities db = new dishupEntities();

        // GET: CustomUnit
        public ActionResult Index()
        {
            var cUSTOM_UNIT = db.CUSTOM_UNIT.Include(c => c.MEDIDA);
            return View(cUSTOM_UNIT.ToList());
        }

        // GET: CustomUnit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOM_UNIT cUSTOM_UNIT = db.CUSTOM_UNIT.Find(id);
            if (cUSTOM_UNIT == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOM_UNIT);
        }

        // GET: CustomUnit/Create
        public ActionResult Create()
        {
            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            return View();
        }

        // POST: CustomUnit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CUSTOM_UNIT,ID_PORTIONED_BY,NAME,QUANTITY,ACTIVE")] CUSTOM_UNIT cUSTOM_UNIT)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOM_UNIT.Add(cUSTOM_UNIT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", cUSTOM_UNIT.ID_PORTIONED_BY);
            return View(cUSTOM_UNIT);
        }

        // GET: CustomUnit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOM_UNIT cUSTOM_UNIT = db.CUSTOM_UNIT.Find(id);
            if (cUSTOM_UNIT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", cUSTOM_UNIT.ID_PORTIONED_BY);
            return View(cUSTOM_UNIT);
        }

        // POST: CustomUnit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CUSTOM_UNIT,ID_PORTIONED_BY,NAME,QUANTITY,ACTIVE")] CUSTOM_UNIT cUSTOM_UNIT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUSTOM_UNIT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", cUSTOM_UNIT.ID_PORTIONED_BY);
            return View(cUSTOM_UNIT);
        }

        // GET: CustomUnit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOM_UNIT cUSTOM_UNIT = db.CUSTOM_UNIT.Find(id);
            if (cUSTOM_UNIT == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOM_UNIT);
        }

        // POST: CustomUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUSTOM_UNIT cUSTOM_UNIT = db.CUSTOM_UNIT.Find(id);
            db.CUSTOM_UNIT.Remove(cUSTOM_UNIT);
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
