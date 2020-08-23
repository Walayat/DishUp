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
    public class PackagingSuppliesController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        // GET: PackagingSupplies
        public ActionResult Index()
        {
            var iNSUMO_PACKAGING = db.INSUMO_PACKAGING.Include(i => i.INSUMO).Include(i => i.PACKAGING).Include(i=>i.INSUMO.MEDIDA);
            return View(iNSUMO_PACKAGING.ToList());
        }

        // GET: PackagingSupplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSUMO_PACKAGING iNSUMO_PACKAGING = db.INSUMO_PACKAGING.Find(id);
            if (iNSUMO_PACKAGING == null)
            {
                return HttpNotFound();
            }
            return View(iNSUMO_PACKAGING);
        }

        // GET: PackagingSupplies/Create
        public ActionResult Create()
        {
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME");
            return View();
        }

        // POST: PackagingSupplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INSUMO_PACKAGING,ID_INSUMO,ID_PACKAGING,QUANTITY,maxPerson,minPerson,ACTIVE")] INSUMO_PACKAGING iNSUMO_PACKAGING)
        {
            if (ModelState.IsValid)
            {
                db.INSUMO_PACKAGING.Add(iNSUMO_PACKAGING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE", iNSUMO_PACKAGING.ID_INSUMO);
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME", iNSUMO_PACKAGING.ID_PACKAGING);
            return View(iNSUMO_PACKAGING);
        }

        // GET: PackagingSupplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSUMO_PACKAGING iNSUMO_PACKAGING = db.INSUMO_PACKAGING.Find(id);
            if (iNSUMO_PACKAGING == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE", iNSUMO_PACKAGING.ID_INSUMO);
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME", iNSUMO_PACKAGING.ID_PACKAGING);
            return View(iNSUMO_PACKAGING);
        }

        // POST: PackagingSupplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INSUMO_PACKAGING,ID_INSUMO,ID_PACKAGING,QUANTITY,maxPerson,minPerson,ACTIVE")] INSUMO_PACKAGING iNSUMO_PACKAGING)
        {
            if (ModelState.IsValid)
            {
                iNSUMO_PACKAGING.ACTIVE = 1;
                db.Entry(iNSUMO_PACKAGING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE", iNSUMO_PACKAGING.ID_INSUMO);
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME", iNSUMO_PACKAGING.ID_PACKAGING);
            return View(iNSUMO_PACKAGING);
        }

        // GET: PackagingSupplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSUMO_PACKAGING iNSUMO_PACKAGING = db.INSUMO_PACKAGING.Find(id);
            if (iNSUMO_PACKAGING == null)
            {
                return HttpNotFound();
            }
            return View(iNSUMO_PACKAGING);
        }

        // POST: PackagingSupplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INSUMO_PACKAGING iNSUMO_PACKAGING = db.INSUMO_PACKAGING.Find(id);
            iNSUMO_PACKAGING.ACTIVE = 0;
            db.Entry(iNSUMO_PACKAGING).State = EntityState.Modified;
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
