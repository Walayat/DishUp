using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DishUp;

namespace DishUp.Controllers
{
    public class PackagingController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        // GET: Packaging
        public ActionResult Index()
        {
            return View(db.PACKAGINGs.ToList());
        }

        // GET: Packaging/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGING pACKAGING = db.PACKAGINGs.Find(id);
            if (pACKAGING == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGING);
        }

        // GET: Packaging/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packaging/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PACKAGING,IMAGE,NAME,UNIT_PRICE,WHOLESALE_PRICE,ACTIVE")] PACKAGING pACKAGING)
        {
            if (ModelState.IsValid)
            {
                db.PACKAGINGs.Add(pACKAGING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pACKAGING);
        }

        // GET: Packaging/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGING pACKAGING = db.PACKAGINGs.Find(id);
            if (pACKAGING == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGING);
        }

        // POST: Packaging/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PACKAGING,IMAGE,NAME,UNIT_PRICE,WHOLESALE_PRICE,ACTIVE")] PACKAGING pACKAGING)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pACKAGING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pACKAGING);
        }

        // GET: Packaging/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PACKAGING pACKAGING = db.PACKAGINGs.Find(id);
            if (pACKAGING == null)
            {
                return HttpNotFound();
            }
            return View(pACKAGING);
        }
        public ActionResult GetImage(int ID)
        {
            String imgSource = db.PACKAGINGs.Where(x => x.ID_PACKAGING == ID).Select(x => x.IMAGE).First();
            return PartialView("_Uploader", imgSource);
        }
        public ActionResult UploadFile(FineUpload upload, int? id)
        {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader
            //  return View();
            PACKAGING pr = db.PACKAGINGs.Where(x => x.ID_PACKAGING == id).First();

            var dir = Server.MapPath("~/UploadPictures/");
            var filePath = Path.Combine(dir, upload.Filename);
            pr.IMAGE = "../../UploadPictures/" + upload.Filename;
            db.Entry(pr).State = EntityState.Modified;
            db.SaveChanges();
            try
            {
                upload.SaveAs(filePath);

            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

            //// the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = pr.IMAGE });
        }

        // POST: Packaging/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PACKAGING pACKAGING = db.PACKAGINGs.Find(id);
            db.PACKAGINGs.Remove(pACKAGING);
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
