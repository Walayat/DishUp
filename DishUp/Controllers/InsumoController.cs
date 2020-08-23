using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DishUp;
using Microsoft.AspNet.Identity;

namespace DishUp.Controllers
{
    public class InsumoController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        // GET: Insumo
        public ActionResult Index()
        {

            var id = User.Identity.GetUserId();
            var iNSUMOes = db.USER_SUPPLIER_INSUMO.Include(i => i.MEDIDA).Include(i => i.MEDIDA1).Where(x=>x.ID_USER == id && x.ACTIVE ==true);
            return View(iNSUMOes.ToList());
        }

        // GET: Insumo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_SUPPLIER_INSUMO iNSUMO = db.USER_SUPPLIER_INSUMO.Find(id);
            if (iNSUMO == null)
            {
                return HttpNotFound();
            }
            return View(iNSUMO);
        }

        // GET: Insumo/Create
        public ActionResult Create()
        {
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            ViewBag.ID_USER_SUPPLIER = new SelectList(db.USER_SUPPLIER, "ID_USER_SUPPLIER", "NAME");
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME");
            return View(new USER_SUPPLIER_INSUMO());
        }

        // POST: Insumo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_MEDIDA_PACKAGE,ID_USER_SUPPLIER,ID_CUSTOM_UNIT,UNIT_WEIGHT,ID_,NAME,PRICE,QUANTITY_PACKAGE,IS_BOX,IS_PACKAGE,QUANTITY_BOX,QUANTITY,CODE")] USER_SUPPLIER_INSUMO iNSUMO, string submitButton)
        {
            bool isBox = false;
            decimal individualPrice;

            if (ModelState.IsValid)
            {
                iNSUMO.ID_USER = User.Identity.GetUserId();
                iNSUMO.ACTIVE = true;

                if (iNSUMO.QUANTITY == null)
                {
                    iNSUMO.QUANTITY = 1;
                }
                
                
                if (iNSUMO.IS_BOX == true) 
                {
                    /*
                     If this option is checked it means the product is sold in a box/package  with multiple items inside. For example: A box of Wine that brings 6 bottles of wine inside.
                     So for this case the information should be input as follows:

                    NAME: BOX OF WINE.
                    QUANTITY: 1 ( Since it's 1 box) 
                    QUANTITY_BOX: 6.
                    PRICE: $x price.
                    
                    Another example would be 1 box of burgers that contains 6 packages of 4 burgers each, with a total of 24 burgers. So the information should be input as follows:

                    NAME: BOX OF BURGUER.
                    QUANTITY: 4 ( This is the amount that brings each package.) 
                    QUANTITY_BOX: 6. ( It comes with 6 packages)
                    PRICE: $x price.
                    
                    */

                    isBox = true;
                    decimal totalQuantity = Convert.ToDecimal((iNSUMO.QUANTITY * iNSUMO.QUANTITY_BOX));
                    individualPrice = Convert.ToDecimal(iNSUMO.PRICE / totalQuantity);
                }
                else
                {
                    individualPrice = Convert.ToDecimal(iNSUMO.PRICE / iNSUMO.QUANTITY);
                }
                  
                iNSUMO.INDIVIDUAL_PRICE = individualPrice;
                db.USER_SUPPLIER_INSUMO.Add(iNSUMO);

            
                db.SaveChanges();
                Success(string.Format(" The ingredient <b>{0}</b> was successfully added to the database.", iNSUMO.NAME), true);
                switch (submitButton)
                {
                    case "Save and Create":
                        return RedirectToAction("Create");

                        break;
                    case "Save":
                        return RedirectToAction("Index");

                        break;
                }
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", iNSUMO.ID_MEDIDA_PACKAGE);
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME");

            return View(iNSUMO);
        }

        // GET: Insumo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_SUPPLIER_INSUMO iNSUMO = db.USER_SUPPLIER_INSUMO.Find(id);
            if (iNSUMO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", iNSUMO.ID_MEDIDA_PACKAGE);
            ViewBag.ID_USER_SUPPLIER = new SelectList(db.USER_SUPPLIER, "ID_USER_SUPPLIER", "NAME", iNSUMO.ID_USER_SUPPLIER);
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME",iNSUMO.ID_CUSTOM_UNIT);

            return View(iNSUMO);
        }

        // POST: Insumo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USER_SUPPLIER,ID_USER_SUPPLIER_INSUMO,UNIT_WEIGHT,ID_MEDIDA_PACKAGE,ID_CUSTOM_UNIT,NAME,PRICE,QUANTITY_PACKAGE,IS_BOX,IS_PACKAGE,QUANTITY_BOX,QUANTITY,CODE")] USER_SUPPLIER_INSUMO iNSUMO)
        {

           
            if (ModelState.IsValid)
            {
         
                var idUser = User.Identity.GetUserId();
                iNSUMO.ID_USER = idUser;
                iNSUMO.ACTIVE = true;
                if(iNSUMO.QUANTITY == null)
                {
                    iNSUMO.QUANTITY = 1;
                }
                bool isBox = false;
                decimal individualPrice;
                if (iNSUMO.IS_BOX == true)
                {
                    isBox = true;
                    decimal totalQuantity = Convert.ToDecimal((iNSUMO.QUANTITY * iNSUMO.QUANTITY_BOX));
                    individualPrice = Convert.ToDecimal(iNSUMO.PRICE / totalQuantity);
                }
                else
                {
                    individualPrice = Convert.ToDecimal(iNSUMO.PRICE / iNSUMO.QUANTITY);
                }
                iNSUMO.INDIVIDUAL_PRICE = individualPrice;
                    db.Entry(iNSUMO).State = EntityState.Modified;
                db.SaveChanges();
                Success(String.Format("The ingredient {0} was updated correctly.", iNSUMO.NAME));

                return RedirectToAction("Index");
            }
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", iNSUMO.ID_MEDIDA_PACKAGE);
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME", iNSUMO.ID_MEDIDA_PACKAGE);
            ViewBag.ID_USER_SUPPLIER = new SelectList(db.USER_SUPPLIER, "ID_USER_SUPPLIER", "NAME", iNSUMO.ID_USER_SUPPLIER);

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            StringBuilder str = new StringBuilder();
            str.AppendLine("The following error has ocurred: <br/>");
            str.AppendLine("<ul>");

            foreach (var it in allErrors)
            {
                str.AppendLine("<li>" + it.ErrorMessage.ToString() + "</li>");
            }
            str.AppendLine("</ul>");

            Danger(str.ToString(), true);
            return View(iNSUMO);
        }

        // GET: Insumo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_SUPPLIER_INSUMO iNSUMO = db.USER_SUPPLIER_INSUMO.Find(id);
            if (iNSUMO == null)
            {
                return HttpNotFound();
            }
            return View(iNSUMO);
        }

        // POST: Insumo/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            USER_SUPPLIER_INSUMO iNSUMO = db.USER_SUPPLIER_INSUMO.Find(id);
            iNSUMO.ACTIVE = false;
            db.Entry(iNSUMO).State = EntityState.Modified;
            db.SaveChanges();
            Success(String.Format("The ingredient {0} was removed correctly.", iNSUMO.NAME));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetSupplierRelated(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "SUPPLIER_SUPLIE_RELATED");

            return Json(result, JsonRequestBehavior.AllowGet);
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
