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
using DishUp.Models;
using Microsoft.AspNet.Identity;

namespace DishUp.Controllers
{
    public class SupplierController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        #region SUPPLIER   GENERAL

        // GET: Supplier
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            return View(db.USER_SUPPLIER.Where(x => x.ACTIVE == true && x.ID_USER == id).ToList());
        }
        public ActionResult Suppliers()
        {
            return View(db.SUPPLIERs.Where(x => x.ACTIVE == true).ToList());
        }

        public ActionResult MySuppliers()
        {
            var id = User.Identity.GetUserId();
            return View("_mySuppliers", db.USER_SUPPLIER.Where(x => x.ACTIVE == true && x.ID_USER == id).ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // GET: Supplier/Create

        public ActionResult Create(int i)
        {
            if (i > 0)
            {
                SupplierViewModel s = new SupplierViewModel { IS_FROM_USER = i };
                return View(s);

            }
            else
            {
                SupplierViewModel s = new SupplierViewModel { IS_FROM_USER = 0 };

                return View(s);
            }

        }
        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NAME,SURNAME,MOBILE_NUMBER,PHONE_NUMBER,ABN_NUMBER,ACN_NUMBER,EMAIL,OPEN_HOURS,DELIVERY_DAYS,ACTIVE,IS_FROM_USER")] SupplierViewModel sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                if (sUPPLIER.IS_FROM_USER == 1)
                {


                    USER_SUPPLIER sup2 = new USER_SUPPLIER
                    {
                        ID_USER = User.Identity.GetUserId(),
                        ID_SUPPLIER = null,
                        NAME = sUPPLIER.NAME,
                        SURNAME = sUPPLIER.SURNAME,
                        MOBILE_NUMBER = sUPPLIER.MOBILE_NUMBER,
                        PHONE_NUMBER = sUPPLIER.PHONE_NUMBER,
                        ABN_NUMBER = sUPPLIER.ABN_NUMBER,
                        ACN_NUMBER = sUPPLIER.ACN_NUMBER,
                        EMAIL = sUPPLIER.EMAIL,
                        OPEN_HOURS = sUPPLIER.OPEN_HOURS,
                        ACTIVE = true
                    };
                    db.USER_SUPPLIER.Add(sup2);
                    db.SaveChanges();
                    Success(string.Format(" The Client <b>{0} {1} </b> was successfully added to the database.", sUPPLIER.NAME, sUPPLIER.SURNAME), true);
                    return RedirectToAction("Index");

                }
                else
                {
                    SUPPLIER sup = new SUPPLIER
                    {
                        ID_SUPPLIER = sUPPLIER.ID_SUPPLIER,
                        NAME = sUPPLIER.NAME,
                        SURNAME = sUPPLIER.SURNAME,
                        MOBILE_NUMBER = sUPPLIER.MOBILE_NUMBER,
                        PHONE_NUMBER = sUPPLIER.PHONE_NUMBER,
                        ABN_NUMBER = sUPPLIER.ABN_NUMBER,
                        ACN_NUMBER = sUPPLIER.ACN_NUMBER,
                        EMAIL = sUPPLIER.EMAIL,
                        OPEN_HOURS = sUPPLIER.OPEN_HOURS,
                        DELIVERY_DAYS = sUPPLIER.DELIVERY_DAYS,
                        ACTIVE = true
                    };
                    db.SUPPLIERs.Add(sup);
                    db.SaveChanges();
                    Success(string.Format(" The Client <b>{0} {1} </b> was successfully added to the database.", sUPPLIER.NAME, sUPPLIER.SURNAME), true);
                    return RedirectToAction("Suppliers");

                }



            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return View(sUPPLIER);
        }


        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            SupplierViewModel sup = sUPPLIER;

            return View(sup);
        }
        public ActionResult EditSupplier(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_SUPPLIER sUPPLIER = db.USER_SUPPLIER.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            SupplierViewModel sup = sUPPLIER;

            return View("EditMySupplier", sup);
        }
        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SUPPLIER,NAME,SURNAME,MOBILE_NUMBER,PHONE_NUMBER,ABN_NUMBER,ACN_NUMBER,EMAIL,OPEN_HOURS,DELIVERY_DAYS,ACTIVE")] SUPPLIER sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                sUPPLIER.ACTIVE = true;
                ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

                db.Entry(sUPPLIER).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format(" The Supplier <b>{0} {1} </b> was successfully updated to the database.", sUPPLIER.NAME, sUPPLIER.SURNAME), true);

                return RedirectToAction("Suppliers");
            }
            return View(sUPPLIER);
        }

        public ActionResult DeleteSupplier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_SUPPLIER sUPPLIER = db.USER_SUPPLIER.Find(id);
            sUPPLIER.ACTIVE = false;
            db.Entry(sUPPLIER).State = EntityState.Modified;
            db.SaveChanges();
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            Success(string.Format(" The supplier <b>{0} {1} </b> was removed succesfully.", sUPPLIER.NAME, sUPPLIER.SURNAME), true);

            return RedirectToAction("Index");

        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            sUPPLIER.ACTIVE = false;
            db.Entry(sUPPLIER).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region #SUPPLIER PERSONAL

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMySupplier([Bind(Include = "ID_SUPPLIER,NAME,SURNAME,MOBILE_NUMBER,PHONE_NUMBER,ABN_NUMBER,ACN_NUMBER,EMAIL,OPEN_HOURS,DELIVERY_DAYS,ACTIVE")] SupplierViewModel sUPPLIER)
        {
            var id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                USER_SUPPLIER sup = new USER_SUPPLIER
                {
                    ID_USER = id,
                    ID_SUPPLIER = sUPPLIER.ID_SUPPLIER,
                    NAME = sUPPLIER.NAME,
                    SURNAME = sUPPLIER.SURNAME,
                    MOBILE_NUMBER = sUPPLIER.MOBILE_NUMBER,
                    PHONE_NUMBER = sUPPLIER.PHONE_NUMBER,
                    ABN_NUMBER = sUPPLIER.ABN_NUMBER,
                    ACN_NUMBER = sUPPLIER.ACN_NUMBER,
                    EMAIL = sUPPLIER.EMAIL,
                    OPEN_HOURS = sUPPLIER.OPEN_HOURS,
                    ACTIVE = true
                };

                db.USER_SUPPLIER.Add(sup);
                db.SaveChanges();
                Success(string.Format(" The supplier <b>{0} {1} </b> was successfully added to the database.", sUPPLIER.NAME, sUPPLIER.SURNAME), true);

                return RedirectToAction("Index");
            }
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
            return View(sUPPLIER);
        }


        [HttpPost]
        //public ActionResult EditMySupplier([Bind(Include = "ID_SUPPLIER,NAME,SURNAME,MOBILE_NUMBER,PHONE_NUMBER,ABN_NUMBER,ACN_NUMBER,EMAIL,OPEN_HOURS,ACTIVE")] USER_SUPPLIER sUPPLIER)
        public ActionResult EditMySupplier([Bind(Include = "ID_USER_SUPPLIER,NAME,SURNAME,MOBILE_NUMBER,PHONE_NUMBER,ABN_NUMBER,ACN_NUMBER,EMAIL,OPEN_HOURS,DELIVERY_DAYS")] USER_SUPPLIER sUPPLIER)
        {
            if (ModelState.IsValid)
            {
                var iduser = User.Identity.GetUserId();
                sUPPLIER.ACTIVE = true;
                sUPPLIER.ID_USER = iduser;
                ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

                db.Entry(sUPPLIER).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format(" The supplier <b>{0} </b> was updated  succesfully .", sUPPLIER.NAME), true);

                return RedirectToAction("Index");
            }
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
            return View(sUPPLIER);
        }
        // GET: Supplier/Delete/5


        #endregion

        #region SUPPLIES GENERAL
        //EDIT


        [HttpGet]
        public ActionResult EditSupplie(int id)
        {

            var sup = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER_INSUMO == id).FirstOrDefault();
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME");
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            if (sup != null)
            {
                SupplierInsumoViewModel supvm = sup;

                ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE", sup.ID_INSUMO);
                ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", sup.ID_MEDIDA_PACKAGE);
                ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", sup.ID_MEDIDA_PACKAGE);

                return View("NewSupplie", supvm);
            }
            else
            {
                ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
                ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
                ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

                return View("NewSupplie", new SupplierInsumoViewModel { ID_SUPPLIER = id });

            }

        }
        [HttpGet]
        public ActionResult AddSupplie(int ID)
        {
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME");

            return View("NewSupplie", new SupplierInsumoViewModel { ID_SUPPLIER = ID });

        }
        [HttpPost]
        public ActionResult DeleteSupplie(int id)
        {
            SUPPLIER_INSUMO sUPPLIER = db.SUPPLIER_INSUMO.Find(id);
            sUPPLIER.ACTIVE = false;
            db.Entry(sUPPLIER).State = EntityState.Modified;

            db.SaveChanges();
            var supplierList = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER == sUPPLIER.ID_SUPPLIER && x.ACTIVE == true).ToList();

            return PartialView("_suppliesList", supplierList);

        }

        [HttpPost]
        public ActionResult EditSupplie(FormCollection fomr)
        {
            if (fomr["ID_SUPPLIER_INSUMO"] != "0")
            {
                var supplie = db.SUPPLIER_INSUMO.Find(Convert.ToInt16(fomr["ID_SUPPLIER_INSUMO"]));

                supplie.CODE = fomr["CODE"];
                
                if (!string.IsNullOrWhiteSpace(fomr["ID_CUSTOM_UNIT"]))
                {
                    supplie.ID_CUSTOM_UNIT = Convert.ToInt32(fomr["ID_CUSTOM_UNIT"]);
                }

                //supplie.ID_INSUMO = Convert.ToInt16(fomr["ID_INSUMO"]);
                supplie.ID_MEDIDA_PACKAGE = Convert.ToInt16(fomr["ID_MEDIDA_PACKAGE"]);
                supplie.ID_SUPPLIER = Convert.ToInt32(fomr["ID_SUPPLIER"]);
                supplie.PRICE = Convert.ToDecimal(fomr["PRICE"]);
                supplie.NAME = fomr["NAME"];

                if (!string.IsNullOrWhiteSpace(fomr["QUANTITY_PACKAGE"]))
                {
                    supplie.QUANTITY_PACKAGE = Convert.ToInt32(fomr["QUANTITY_PACKAGE"]);
                }

                double price = Convert.ToDouble(fomr["PRICE"]);
                double individualPrice = 0;
                double packagePrice = 0;
                bool? isBox = false, isPackage = false;

                if (fomr["IS_BOX"] == "on")
                {
                    isBox = true;
                    if (fomr["IS_PACKAGE"] == "on")
                    {
                        isPackage = true;

                        packagePrice = (price / Convert.ToInt16(fomr["QUANTITY_BOX"]));
                        individualPrice = packagePrice / Convert.ToInt16(fomr["QUANTITY_PACKAGE"]);

                    }
                    else
                    {
                        individualPrice = price / Convert.ToInt16(fomr["QUANTITY_BOX"]);

                    }
                }
                else
                {
                    if (fomr["IS_BOX"] == "on")
                    {
                        isPackage = true;

                        packagePrice = price;
                        individualPrice = packagePrice / Convert.ToInt16(fomr["QUANTITY_PACKAGE"]);

                    }
                }

                supplie.IS_BOX = isBox;
                supplie.IS_PACKAGE = isPackage;
                db.Entry(supplie).State = EntityState.Modified;
                db.SaveChanges();

                var ID_SUPPLIER = Convert.ToInt16(fomr["ID_SUPPLIER"]);
                //var supplierList = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER == ID_SUPPLIER).ToList();
                Success(String.Format("The Supplie {0} has been updated successfully", fomr["NOMBRE"]));

                return RedirectToAction("Suppliers");
            }
            else
            {
                double price = Convert.ToDouble(fomr["PRICE"]);
                double individualPrice = 0;
                double packagePrice = 0;
                bool? isBox = false, isPackage = false;

                if (fomr["IS_BOX"] == "on")
                {
                    isBox = true;
                    if (fomr["IS_PACKAGE"] == "on")
                    {
                        isPackage = true;

                        packagePrice = (price / Convert.ToInt16(fomr["QUANTITY_BOX"]));
                        individualPrice = packagePrice / Convert.ToInt16(fomr["QUANTITY_PACKAGE"]);

                    }
                    else
                    {
                        individualPrice = price / Convert.ToInt16(fomr["QUANTITY_BOX"]);

                    }
                }
                else
                {
                    if (fomr["IS_BOX"] == "on")
                    {
                        isPackage = true;

                        packagePrice = price;
                        individualPrice = packagePrice / Convert.ToInt16(fomr["QUANTITY_PACKAGE"]);

                    }
                }
                SUPPLIER_INSUMO s = new SUPPLIER_INSUMO
                {
                    CODE = fomr["CODE"],

                    //ID_INSUMO = Convert.ToInt16(fomr["ID_INSUMO"]);
                    ID_CUSTOM_UNIT = Convert.ToInt32(fomr["ID_CUSTOM_UNIT"]),
                    ID_MEDIDA_PACKAGE = Convert.ToInt16(fomr["ID_MEDIDA_PACKAGE"]),
                    ID_SUPPLIER = Convert.ToInt16(fomr["ID_SUPPLIER"]),
                    PRICE = Convert.ToDecimal(fomr["PRICE"]),
                    NAME = fomr["NAME"],
                    ACTIVE = true,
                    IS_BOX = isBox,
                    IS_PACKAGE = isPackage,
                    QUANTITY_PACKAGE = Convert.ToInt32(fomr["QUANTITY_PACKAGE"])

                };
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();




                var supplierList = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER == Convert.ToInt16(fomr["ID_SUPPLIER"])).ToList();
                return PartialView("_suppliesList", supplierList);

            }


        }


        [HttpPost]
        public ActionResult AddSupplie([Bind(Include = "ID_SUPPLIER_INSUMO,ID_CUSTOM_UNIT,ID_MEDIDA_PACKAGE,IS_PACKAGE,IS_BOX,QUANTITY_BOX,QUANTITY_PACKAGE,PRICE,CODE,SELECTED,NAME,ID_SUPPLIER")] SupplierInsumoViewModel sUPPLIER, FormCollection fc)
        //public ActionResult AddSupplie  (FormCollection fc)
        {
            //INSUMO ins = new INSUMO();
            int quant = 1;
            var idSupplier = Convert.ToInt32(fc["ID_SUPPLIER"]);
            SUPPLIER sup = db.SUPPLIERs.Find(idSupplier);
            bool? isPackage = null;
            bool? isBox = null;


            if (ModelState.IsValid)
            {
                if (fc["ID_SUPPLIER_INSUMO"] == "0")
                {

                    //if (fc["addMyIngredients"] == "on") //ADD PRODUCT FROM SUPPLIER TO INGREDIENTS.
                    //{
                    //    //ins.ID_MEDIDA = Convert.ToInt32(fc["ID_MEDIDA_PACKAGE"]);
                    //    //ins.NOMBRE = fc["NAME"];
                    //    var a = fc["quantity_package"];
                    //    if (fc["quantity_package"] == "")
                    //    {
                    //        //ins.QUANTITY = 1;
                    //        quant = 1;
                    //    }
                    //    else
                    //    {
                    //        ins.QUANTITY = Convert.ToInt32(fc["quantity_package"]);
                    //        quant = Convert.ToInt32(fc["quantity_package"]);
                    //    }
                    //    //ins.WHOLESALE_PRICE = Convert.ToDecimal(fc["wholesale"]);
                    //    //ins.ACTIVE = true;
                    //    //ins.PRICE_PER_UNIT = Convert.ToDecimal(fc["wholesale"]) / Convert.ToDecimal(quant);
                    //    //db.INSUMOes.Add(ins);
                    //}

                    double price = Convert.ToDouble(fc["PRICE"]);
                    double individualPrice = 0;
                    double packagePrice = 0;


                    /*CALCULATE PRICE INDIVIDUAL / PACKAGE / BOX */

                    if (fc["IS_BOX"] == "on")
                    {
                        isBox = true;
                        if (fc["IS_PACKAGE"] == "on")
                        {
                            isPackage = true;

                            packagePrice = (price / Convert.ToInt16(fc["QUANTITY_BOX"]));
                            individualPrice = packagePrice / Convert.ToInt16(fc["QUANTITY_PACKAGE"]);

                        }
                        else
                        {
                            individualPrice = price / Convert.ToInt16(fc["QUANTITY_BOX"]);

                        }
                    }
                    else
                    {
                        if (fc["IS_BOX"] == "on")
                        {
                            isPackage = true;

                            packagePrice = price;
                            individualPrice = packagePrice / Convert.ToInt16(fc["QUANTITY_PACKAGE"]);

                        }
                    }

                    db.SUPPLIER_INSUMO.Add(new SUPPLIER_INSUMO
                    {
                        ID_MEDIDA_PACKAGE = Convert.ToInt32(fc["ID_MEDIDA_PACKAGE"]), // PURCHASE UNIT
                        ID_CUSTOM_UNIT = Convert.ToInt32(fc["ID_CUSTOM_UNIT"]), // CUSTOM UNIT
                        ID_INSUMO = null,
                        IS_PACKAGE = isPackage,
                        IS_BOX = isBox,
                        QUANTITY_BOX = Convert.ToInt32(fc["quantity_box"]),
                        CODE = fc["CODE"].ToString(),
                        PRICE = Convert.ToDecimal(fc["PRICE"]),
                        QUANTITY_PACKAGE = Convert.ToInt32(fc["quantity_package"]),
                        ACTIVE = true,
                        ID_SUPPLIER = Convert.ToInt32(fc["ID_SUPPLIER"]),
                        SELECTED = true,
                        NAME = fc["NAME"]
                    });
                    db.SaveChanges();
                    var listaInsumos = db.SUPPLIER_INSUMO.Include(x => x.INSUMO.MEDIDA).Where(x => x.ID_SUPPLIER == idSupplier).ToList();
                    Success(String.Format("The Supplie {0} has been added successfully", fc["NOMBRE"]));

                    return RedirectToAction("Edit", new { id = idSupplier });


                }
                else
                {
                    var id = Convert.ToInt32(fc["ID_SUPPLIER_INSUMO"]);
                    SUPPLIER_INSUMO si = db.SUPPLIER_INSUMO.Find(id);
                    double price = Convert.ToDouble(fc["PRICE"]);
                    double individualPrice = 0;
                    double packagePrice = 0;
                    if (fc["isBox"] == "on")
                    {
                        isBox = true;
                        if (fc["isPackage"] == "on")
                        {
                            isPackage = true;

                            packagePrice = (price / Convert.ToInt16(fc["QUANTITY_BOX"]));
                            individualPrice = packagePrice / Convert.ToInt16(fc["QUANTITY_PACKAGE"]);

                        }
                        else
                        {
                            individualPrice = price / Convert.ToInt16(fc["QUANTITY_BOX"]);

                        }
                    }
                    else
                    {
                        if (fc["isPackage"] == "on")
                        {
                            isPackage = true;

                            packagePrice = price;
                            individualPrice = packagePrice / Convert.ToInt16(fc["QUANTITY_PACKAGE"]);

                        }
                    }

                    si.ID_MEDIDA_PACKAGE = Convert.ToInt32(fc["ID_MEDIDA"]);
                    si.ID_CUSTOM_UNIT = Convert.ToInt32(fc["ID_CUSTOM_UNIT"]);
                    //  ID_INSUMO = ins.ID_INSUMO;
                    si.IS_PACKAGE = isPackage;
                    si.IS_BOX = isBox;
                    si.QUANTITY_BOX = Convert.ToInt32(fc["quantity_box"]);
                    si.CODE = fc["CODE"];
                    si.PRICE = Convert.ToDecimal(fc["PRICE"]);
                    si.QUANTITY_PACKAGE = Convert.ToInt32(fc["QUANTITY_PACKAGE"]);
                    si.ACTIVE = true;
                    si.ID_SUPPLIER = Convert.ToInt32(fc["ID_SUPPLIER"]);
                    si.SELECTED = true;
                    si.NAME = fc["NAME"];

                    db.Entry(si).State = EntityState.Modified;
                    db.SaveChanges();
                    var listaInsumos = db.SUPPLIER_INSUMO.Include(x => x.INSUMO.MEDIDA).Where(x => x.ID_SUPPLIER == idSupplier).ToList();
                    Success(String.Format("The Supplie {0} has been edited successfully", fc["NOMBRE"]));

                    return PartialView("_suppliesList", listaInsumos);

                }
            }





            var listaInsumos2 = db.SUPPLIER_INSUMO.Include(x => x.INSUMO.MEDIDA).Where(x => x.ID_SUPPLIER == idSupplier).ToList();
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", sUPPLIER.ID_MEDIDA_PACKAGE);
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME", sUPPLIER.ID_CUSTOM_UNIT);

            StringBuilder str = new StringBuilder();
            str.AppendLine("The following error has ocurred: <br/>");
            str.AppendLine("<ul>");

            foreach (var it in allErrors)
            {
                str.AppendLine("<li>" + it.ErrorMessage.ToString() + "</li>");
            }
            str.AppendLine("</ul>");

            Danger(str.ToString(), true);
            return View("NewSupplie", sUPPLIER);

        }

        #endregion

        #region SUPPLIES PERSONAL
        [HttpGet]
        public ActionResult EditMySupplie(int id)
        {

            var sup = db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER_SUPPLIER_INSUMO == id).FirstOrDefault();
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", sup.ID_MEDIDA_PACKAGE);
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME", sup.ID_CUSTOM_UNIT);

            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", sup.ID_MEDIDA_PACKAGE);
            return View("NewMySupplie", sup);


        }
        [HttpPost]
        public ActionResult EditMySupplie(FormCollection fomr)
        {
            var id = User.Identity.GetUserId();
            if (fomr["ID_USER_SUPPLIER_INSUMO"] != "0")
            {
                var supplie = db.USER_SUPPLIER_INSUMO.Find(Convert.ToInt16(fomr["ID_USER_SUPPLIER_INSUMO"]));


                var quantity = Convert.ToInt16(fomr["QUANTITY"]);
                if (quantity == 0)
                {
                    quantity = 1;
                }
                else
                {
                    quantity = Convert.ToInt16(fomr["QUANTITY"]);
                }

                supplie.CODE = fomr["CODE"];
                supplie.ID_USER = id;
                supplie.QUANTITY = quantity;
                supplie.ID_CUSTOM_UNIT = Convert.ToInt32(fomr["ID_CUSTOM_UNIT"]);
                supplie.ID_MEDIDA_PACKAGE = Convert.ToInt32(fomr["ID_MEDIDA_PACKAGE"]);
                supplie.ID_USER_SUPPLIER = Convert.ToInt32(fomr["ID-USER_SUPPLIER"]);
                supplie.PRICE = Convert.ToDecimal(fomr["PRICE"]);
                supplie.NAME = fomr["NAME"];
                supplie.QUANTITY_PACKAGE = Convert.ToInt32(fomr["QUANTITY_PACKAGE"]);

                double price = Convert.ToDouble(fomr["PRICE"]);
                double individualPrice = 0;
                double packagePrice = 0;
                bool? isBox = false, isPackage = false;

                if (fomr["IS_BOX"] == "on")
                {
                    isBox = true;
                    double totalQuantity = quantity * Convert.ToInt16(fomr["QUANTITY_BOX"]);
                    individualPrice = price / totalQuantity;

                }
                else
                {
                    individualPrice = price / quantity;

                }

                supplie.IS_BOX = isBox;
                supplie.IS_PACKAGE = isPackage;
                db.Entry(supplie).State = EntityState.Modified;
                db.SaveChanges();

                var ID_SUPPLIER = Convert.ToInt16(fomr["ID_USER_SUPPLIER"]);
                Success(String.Format("The Supplie {0} has been updated successfully", fomr["NOMBRE"]));

                return RedirectToAction("EditMySupplie", supplie.ID_USER_SUPPLIER_INSUMO);
            }
            else
            {
                double price = Convert.ToDouble(fomr["PRICE"]);
                double individualPrice = 0;
                double packagePrice = 0;
                bool? isBox = false, isPackage = false;

                if (fomr["IS_BOX"] == "on")
                {
                    isBox = true;
                    if (fomr["IS_PACKAGE"] == "on")
                    {
                        isPackage = true;

                        packagePrice = (price / Convert.ToInt16(fomr["QUANTITY_BOX"]));
                        individualPrice = packagePrice / Convert.ToInt16(fomr["QUANTITY_PACKAGE"]);

                    }
                    else
                    {
                        individualPrice = price / Convert.ToInt16(fomr["QUANTITY_BOX"]);

                    }
                }
                else
                {
                    if (fomr["IS_BOX"] == "on")
                    {
                        isPackage = true;

                        packagePrice = price;
                        individualPrice = packagePrice / Convert.ToInt16(fomr["QUANTITY_PACKAGE"]);

                    }
                }
                var quantity = Convert.ToInt16(fomr["QUANTITY"]);
                if (quantity == 0)
                {
                    quantity = 1;
                }
                else
                {
                    quantity = Convert.ToInt16(fomr["QUANTITY"]);
                }
                USER_SUPPLIER_INSUMO s = new USER_SUPPLIER_INSUMO
                {
                    CODE = fomr["CODE"],
                    QUANTITY = quantity,
                    ID_USER = id,
                    //ID_INSUMO = Convert.ToInt16(fomr["ID_INSUMO"]);
                    ID_CUSTOM_UNIT = Convert.ToInt32(fomr["ID_CUSTOM_UNIT"]),
                    ID_MEDIDA_PACKAGE = Convert.ToInt32(fomr["ID_MEDIDA_PACKAGE"]),
                    ID_USER_SUPPLIER = Convert.ToInt16(fomr["ID_USER_SUPPLIER"]),
                    PRICE = Convert.ToDecimal(fomr["PRICE"]),
                    NAME = fomr["NAME"],
                    ACTIVE = true,
                    IS_BOX = isBox,
                    IS_PACKAGE = isPackage,
                    QUANTITY_PACKAGE = Convert.ToInt32(fomr["QUANTITY_PACKAGE"])

                };
                db.USER_SUPPLIER_INSUMO.Add(s);
                db.SaveChanges();

                Success(String.Format("The Supplie {0} has been created successfully", fomr["NOMBRE"]));
                return RedirectToAction("EditMySupplie", s.ID_USER_SUPPLIER_INSUMO);
            }


        }


        [HttpGet]
        public ActionResult AddMySupplie(int ID)
        {
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME");

            return View("NewMySupplie", new USER_SUPPLIER_INSUMO { ID_USER_SUPPLIER = ID });

        }
        public ActionResult AddMySupplie([Bind(Include = "ID_SUPPLIER_INSUMO,ID_CUSTOM_UNIT,ID_MEDIDA_PACKAGE,IS_PACKAGE,IS_BOX,QUANTITY_BOX,QUANTITY_PACKAGE,PRICE,CODE,SELECTED,NAME,ID_SUPPLIER,QUANTITY")] SupplierInsumoViewModel sUPPLIER, FormCollection fc)
        //public ActionResult AddSupplie  (FormCollection fc)
        {
            //INSUMO ins = new INSUMO();
            int quant = 1;
            var idSupplier = Convert.ToInt32(fc["ID_USER_SUPPLIER"]);
            USER_SUPPLIER sup = db.USER_SUPPLIER.Find(idSupplier);
            bool? isPackage = false;
            bool? isBox = false;


            if (ModelState.IsValid)
            {
                if (fc["ID_USER_SUPPLIER_INSUMO"] == "0")
                {
                    double price = Convert.ToDouble(fc["PRICE"]);
                    double individualPrice = 0;
                    double packagePrice = 0;
                    var check = fc["IS_PACKAGE"];                    /*CALCULATE PRICE INDIVIDUAL / PACKAGE / BOX */

                    if (fc["IS_BOX"].Contains("true"))
                    {
                        isBox = true;
                        if (fc["IS_PACKAGE"].Contains("true"))
                        {
                            isPackage = true;

                            packagePrice = (price / Convert.ToInt16(sUPPLIER.QUANTITY_BOX));
                            individualPrice = packagePrice / Convert.ToInt16(sUPPLIER.QUANTITY_PACKAGE);

                        }
                        else
                        {
                            individualPrice = price / Convert.ToInt16(sUPPLIER.QUANTITY_BOX);

                        }
                    }
                    else
                    {
                        if (fc["IS_PACKAGE"].Contains("true"))
                        {
                            isPackage = true;

                            packagePrice = price;
                            individualPrice = packagePrice / Convert.ToInt16(sUPPLIER.QUANTITY_PACKAGE);

                        }
                    }
                    var id = User.Identity.GetUserId();
                    if (sUPPLIER.QUANTITY == 0)
                    {
                        sUPPLIER.QUANTITY = 1;
                    }
                    var y = new USER_SUPPLIER_INSUMO
                    {
                        ID_MEDIDA_PACKAGE = sUPPLIER.ID_MEDIDA_PACKAGE, // PURCHASE UNIT
                        ID_CUSTOM_UNIT = sUPPLIER.ID_CUSTOM_UNIT, // PURCHASE UNIT
                        ID_USER = id,
                        ID_SUPPLIER_INSUMO = sUPPLIER.ID_SUPPLIER_INSUMO,
                        QUANTITY = sUPPLIER.QUANTITY,
                        IS_PACKAGE = isPackage,
                        IS_BOX = isBox,
                        QUANTITY_BOX = sUPPLIER.QUANTITY_BOX,
                        CODE = fc["CODE"],
                        PRICE = sUPPLIER.PRICE,
                        QUANTITY_PACKAGE = sUPPLIER.QUANTITY_PACKAGE,
                        ACTIVE = true,
                        ID_USER_SUPPLIER = Convert.ToInt32(fc["ID_USER_SUPPLIER"]),
                        SELECTED = true,
                        NAME = fc["NAME"],
                        INDIVIDUAL_PRICE = (decimal)individualPrice
                    };
                    db.USER_SUPPLIER_INSUMO.Add(y);
                    db.SaveChanges();
                    var listaInsumos = db.USER_SUPPLIER_INSUMO.Include(x => x.USER_SUPPLIER).Include(x => x.MEDIDA).Where(x => x.ID_USER_SUPPLIER == idSupplier).ToList();

                    return RedirectToAction("EditMySupplie", new { id = y.ID_USER_SUPPLIER_INSUMO });

                }
                else
                {
                    var id = Convert.ToInt32(fc["ID_USER_SUPPLIER_INSUMO"]);
                    var idUser = User.Identity.GetUserId();

                    USER_SUPPLIER_INSUMO si = db.USER_SUPPLIER_INSUMO.Find(id);
                    double price = Convert.ToDouble(fc["PRICE"]);
                    double individualPrice = 0;
                    double packagePrice = 0;
                    if (fc["IS_BOX"].Contains("true"))
                    {
                        isBox = true;
                        if (fc["IS_PACKAGE"].Contains("true"))
                        {
                            isPackage = true;

                            packagePrice = (price / Convert.ToInt16(sUPPLIER.QUANTITY_BOX));
                            individualPrice = packagePrice / Convert.ToInt16(sUPPLIER.QUANTITY_PACKAGE);

                        }
                        else
                        {
                            individualPrice = price / Convert.ToInt16(sUPPLIER.QUANTITY_BOX);

                        }
                    }
                    else
                    {
                        if (fc["IS_PACKAGE"].Contains("true"))
                        {
                            isPackage = true;

                            packagePrice = price;
                            individualPrice = packagePrice / Convert.ToInt16(sUPPLIER.QUANTITY_PACKAGE);

                        }
                    }

                    var dataMedida = fc["ID_MEDIDA_PACKAGE"];
                    var customUnit = fc["ID_CUSTOM_UNIT"];

                    if (!string.IsNullOrWhiteSpace(dataMedida))
                    {
                        si.ID_MEDIDA_PACKAGE = Convert.ToInt32(dataMedida);
                    }
                    if (!string.IsNullOrWhiteSpace(customUnit))
                    {
                        si.ID_CUSTOM_UNIT = Convert.ToInt32(customUnit);
                    }


                    //  ID_INSUMO = ins.ID_INSUMO;
                    si.IS_PACKAGE = isPackage;
                    si.IS_BOX = isBox;
                    si.ID_USER = idUser;
                    si.QUANTITY_BOX = sUPPLIER.QUANTITY_BOX;
                    si.QUANTITY = sUPPLIER.QUANTITY;
                    si.CODE = sUPPLIER.CODE;
                    si.PRICE = sUPPLIER.PRICE;
                    si.QUANTITY_PACKAGE = sUPPLIER.QUANTITY_PACKAGE;
                    si.ACTIVE = true;
                    si.ID_USER_SUPPLIER = Convert.ToInt32(fc["ID_USER_SUPPLIER"]);
                    si.SELECTED = true;
                    si.NAME = fc["NAME"];
                    si.INDIVIDUAL_PRICE = (decimal)individualPrice;
                    si.ID_SUPPLIER_INSUMO = sUPPLIER.ID_SUPPLIER_INSUMO;

                    db.Entry(si).State = EntityState.Modified;
                    db.SaveChanges();
                    var listaInsumos = db.USER_SUPPLIER_INSUMO.Include(x => x.MEDIDA).Where(x => x.ID_USER_SUPPLIER == idSupplier).ToList();
                    Success(String.Format("The Supplie {0} has been edited successfully", fc["NOMBRE"]));
                    return RedirectToAction("EditMySupplie", new { id = si.ID_USER_SUPPLIER_INSUMO });

                }
            }





            var listaInsumos2 = db.SUPPLIER_INSUMO.Include(x => x.INSUMO.MEDIDA).Where(x => x.ID_SUPPLIER == idSupplier).ToList();
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.ID_MEDIDA_PACKAGE = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", sUPPLIER.ID_MEDIDA_PACKAGE);
            ViewBag.ID_CUSTOM_UNIT = new SelectList(db.CUSTOM_UNIT, "ID_CUSTOM_UNIT", "NAME", sUPPLIER.ID_CUSTOM_UNIT);

            StringBuilder str = new StringBuilder();
            str.AppendLine("The following error has ocurred: <br/>");
            str.AppendLine("<ul>");

            foreach (var it in allErrors)
            {
                str.AppendLine("<li>" + it.ErrorMessage.ToString() + "</li>");
            }
            str.AppendLine("</ul>");

            Danger(str.ToString(), true);
            return View("NewSupplie", sUPPLIER);

        }

        public ActionResult GetSuppliesList(int? ID_SUPPLIER)

        {
            var supplies = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER == ID_SUPPLIER && x.ACTIVE == true).ToList();
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            return PartialView("_suppliesList", supplies);

        }
        public ActionResult GetMySuppliesList(int? ID_SUPPLIER)
        {
            var id = User.Identity.GetUserId();
            var supplies = db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER_SUPPLIER == ID_SUPPLIER && x.ACTIVE == true && x.ID_USER == id).ToList();
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            return PartialView("_mySupplieList", supplies);
            //return PartialView("_mySupplieList", supplies);
        }
        #endregion



        [HttpPost]
        public ActionResult AddToMyIngredient(int? ids)
        {
            var id = User.Identity.GetUserId();

            bool flag = false;
            //CHECK IF THE SUPPLIER IS ALREADY ADDED.
            var isAdded = db.USER_SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER_INSUMO == ids && x.ID_USER == id).FirstOrDefault();

            var obj = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER_INSUMO == ids).FirstOrDefault();

            if (isAdded == null)
            {
                var isSupplierAdded = 0;
                if (!(obj is null))
                {

                    isSupplierAdded = db.USER_SUPPLIER.Where(x => x.ID_SUPPLIER == obj.ID_SUPPLIER && x.ID_USER == id).Count();
                }

                if (isSupplierAdded == 0)
                {

                    //IF THE SUPPLIER HASNT BEEN ADDED TO THE USER SUPPLIER THEN WE ADD IT BEFORE ADDING THE PRODUCT.

                    var sup = db.SUPPLIERs.Where(x => x.ID_SUPPLIER == obj.ID_SUPPLIER).First();
                    USER_SUPPLIER usp = new USER_SUPPLIER
                    {
                        ID_SUPPLIER = sup.ID_SUPPLIER,
                        ABN_NUMBER = sup.ABN_NUMBER,
                        ACN_NUMBER = sup.ACN_NUMBER,
                        EMAIL = sup.EMAIL,
                        MOBILE_NUMBER = sup.MOBILE_NUMBER,
                        NAME = sup.NAME,
                        SURNAME = sup.SURNAME,
                        OPEN_HOURS = sup.OPEN_HOURS,
                        PHONE_NUMBER = sup.PHONE_NUMBER,
                        ID_USER = id,
                        ACTIVE = true
                    };
                    db.USER_SUPPLIER.Add(usp);


                    //WE NOW ADD THE PRODUCT USING THE ID FROM THE NEW USER_SUPPLIER THAT WE JUST CREATED.

                    USER_SUPPLIER_INSUMO us = new USER_SUPPLIER_INSUMO
                    {
                        ID_USER = id,
                        ID_SUPPLIER_INSUMO = ids,
                        ACTIVE = true,
                        IS_IMPORTED = true,
                        CODE = obj.CODE,
                        ID_MEDIDA_PACKAGE = obj.ID_MEDIDA_PACKAGE,
                        INDIVIDUAL_PRICE = obj.INDIVIDUAL_PRICE,
                        IS_BOX = obj.IS_BOX,
                        ID_CUSTOM_UNIT = obj.ID_CUSTOM_UNIT,
                        NAME = obj.NAME,
                        PRICE = obj.PRICE,
                        QUANTITY = obj.QUANTITY,
                        QUANTITY_BOX = obj.QUANTITY_BOX,
                        ID_USER_SUPPLIER = usp.ID_USER_SUPPLIER,


                    };
                    db.USER_SUPPLIER_INSUMO.Add(us);

                    db.SaveChanges();
                }
                else
                {
                    // ADD THE PRODUCT TO THE USER WITH THE SUPPLIER RELATED
                    var USP = db.USER_SUPPLIER.Where(x => x.ID_SUPPLIER == obj.ID_SUPPLIER).First();
                    USER_SUPPLIER_INSUMO us = new USER_SUPPLIER_INSUMO
                    {
                        ID_USER = id,
                        ID_SUPPLIER_INSUMO = ids,
                        ACTIVE = true,
                        IS_IMPORTED = true,
                        CODE = obj.CODE,
                        ID_MEDIDA_PACKAGE = obj.ID_MEDIDA_PACKAGE,
                        INDIVIDUAL_PRICE = obj.INDIVIDUAL_PRICE,
                        IS_BOX = obj.IS_BOX,
                        ID_CUSTOM_UNIT = obj.ID_CUSTOM_UNIT,
                        NAME = obj.NAME,
                        PRICE = obj.PRICE,
                        QUANTITY = obj.QUANTITY,
                        QUANTITY_BOX = obj.QUANTITY_BOX,
                        ID_USER_SUPPLIER = USP.ID_USER_SUPPLIER,
                    };
                    db.USER_SUPPLIER_INSUMO.Add(us);
                }

                flag = true;
                db.SaveChanges();
                return Json(new { success = true, message = "Item Added succesfully to your ingredients", Title = "Item Added succesfully to your ingredients!", CssClassName = "alert alert-" + (flag ? "success" : "info") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "", Title = "Oops!, Looks like the item is already on your list", CssClassName = "alert alert-" + (flag ? "success" : "info") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddIngredient(int? ids)
        {
            var id = User.Identity.GetUserId();

            bool flag = false;
            //CHECK IF THE SUPPLIER IS ALREADY ADDED.
            var isAdded = db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER_SUPPLIER_INSUMO == ids && x.ID_USER == id).FirstOrDefault();

            var obj = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER_INSUMO == isAdded.ID_SUPPLIER_INSUMO).FirstOrDefault();

            if (isAdded == null)
            {
                var isSupplierAdded = 0;
                if (!(obj is null))
                {

                   isSupplierAdded = db.USER_SUPPLIER.Where(x => x.ID_SUPPLIER == obj.ID_SUPPLIER && x.ID_USER == id).Count();
                }

                if (isSupplierAdded == 0)
                {

                    //IF THE SUPPLIER HASNT BEEN ADDED TO THE USER SUPPLIER THEN WE ADD IT BEFORE ADDING THE PRODUCT.

                    var sup = db.SUPPLIERs.Where(x => x.ID_SUPPLIER == obj.ID_SUPPLIER).First();
                    USER_SUPPLIER usp = new USER_SUPPLIER
                    {
                        ID_SUPPLIER = sup.ID_SUPPLIER,
                        ABN_NUMBER = sup.ABN_NUMBER,
                        ACN_NUMBER = sup.ACN_NUMBER,
                        EMAIL = sup.EMAIL,
                        MOBILE_NUMBER = sup.MOBILE_NUMBER,
                        NAME = sup.NAME,
                        SURNAME = sup.SURNAME,
                        OPEN_HOURS = sup.OPEN_HOURS,
                        PHONE_NUMBER = sup.PHONE_NUMBER,
                        ID_USER = id,
                        ACTIVE = true
                    };
                    db.USER_SUPPLIER.Add(usp);


                    //WE NOW ADD THE PRODUCT USING THE ID FROM THE NEW USER_SUPPLIER THAT WE JUST CREATED.

                    USER_SUPPLIER_INSUMO us = new USER_SUPPLIER_INSUMO
                    {
                        ID_USER = id,
                        ID_SUPPLIER_INSUMO = ids,
                        ACTIVE = true,
                        IS_IMPORTED = true,
                        CODE = obj.CODE,
                        ID_MEDIDA_PACKAGE = obj.ID_MEDIDA_PACKAGE,
                        INDIVIDUAL_PRICE = obj.INDIVIDUAL_PRICE,
                        IS_BOX = obj.IS_BOX,
                        ID_CUSTOM_UNIT = obj.ID_CUSTOM_UNIT,
                        NAME = obj.NAME,
                        PRICE = obj.PRICE,
                        QUANTITY = obj.QUANTITY,
                        QUANTITY_BOX = obj.QUANTITY_BOX,
                        ID_USER_SUPPLIER = usp.ID_USER_SUPPLIER,


                    };
                    db.USER_SUPPLIER_INSUMO.Add(us);

                    db.SaveChanges();
                } 
                else
                {
                    // ADD THE PRODUCT TO THE USER WITH THE SUPPLIER RELATED
                    var USP = db.USER_SUPPLIER.Where(x => x.ID_SUPPLIER == obj.ID_SUPPLIER).First();
                    USER_SUPPLIER_INSUMO us = new USER_SUPPLIER_INSUMO
                    {
                        ID_USER = id,
                        ID_SUPPLIER_INSUMO = ids,
                        ACTIVE = true,
                        IS_IMPORTED = true,
                        CODE = obj.CODE,
                        ID_MEDIDA_PACKAGE = obj.ID_MEDIDA_PACKAGE,
                        INDIVIDUAL_PRICE = obj.INDIVIDUAL_PRICE,
                        IS_BOX = obj.IS_BOX,
                        ID_CUSTOM_UNIT = obj.ID_CUSTOM_UNIT,
                        NAME = obj.NAME,
                        PRICE = obj.PRICE,
                        QUANTITY = obj.QUANTITY,
                        QUANTITY_BOX = obj.QUANTITY_BOX,
                        ID_USER_SUPPLIER = USP.ID_USER_SUPPLIER,
                    };
                    db.USER_SUPPLIER_INSUMO.Add(us);
                }

                flag = true;
                db.SaveChanges();
                return Json(new { success = true, message = "Item Added succesfully to your ingredients", Title = "Item Added succesfully to your ingredients!", CssClassName = "alert alert-" + (flag ? "success" : "info") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "", Title = "Oops!, Looks like the item is already on your list", CssClassName = "alert alert-" + (flag ? "success" : "info") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddSuplierToUser(int? ids)
        {
            var id = User.Identity.GetUserId();

            bool flag = false;
            var exist = db.USER_SUPPLIER.Where(x => x.ID_SUPPLIER == ids && x.ID_USER == id).Count();
            if (exist == 0)
            {
                flag = true;
                SUPPLIER s = db.SUPPLIERs.Where(x => x.ID_SUPPLIER == ids).First();
                USER_SUPPLIER us = new USER_SUPPLIER

                {
                    ID_USER = id,
                    ID_SUPPLIER = s.ID_SUPPLIER,
                    EMAIL = s.EMAIL,
                    ACN_NUMBER = s.ACN_NUMBER,
                    ABN_NUMBER = s.ABN_NUMBER,
                    MOBILE_NUMBER = s.MOBILE_NUMBER,
                    NAME = s.NAME,
                    OPEN_HOURS = s.OPEN_HOURS,
                    PHONE_NUMBER = s.PHONE_NUMBER,
                    SURNAME = s.SURNAME,

                    ACTIVE = true
                };
                db.USER_SUPPLIER.Add(us);
                db.SaveChanges();
                return Json(new { success = true, message = "Supplier Added succesfully to your ingredients", Title = "Supplier Added succesfully to your ingredients!", CssClassName = "alert alert-" + (flag ? "success" : "info") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "", Title = "Oops!, Looks like the supplier is already on yout list", CssClassName = "alert alert-" + (flag ? "success" : "warning") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportSuppliesFromSupplier(int? ids)
        {
            var id = User.Identity.GetUserId();

            bool flag = false;
            int i = 0;
            var listOfProducts = db.SUPPLIER_INSUMO.Where(x => x.ID_SUPPLIER == ids && x.ACTIVE == true);
            foreach (var it in listOfProducts)
            {
                var exist = db.USER_INSUMO.Where(x => x.ID_SUPPLIER_INSUMO == it.ID_SUPPLIER_INSUMO).Count();
                if (exist == 0)
                {
                    flag = true;
                    i += 1;
                    db.USER_INSUMO.Add(new USER_INSUMO
                    {
                        ID_SUPPLIER_INSUMO = it.ID_SUPPLIER_INSUMO,
                        ID_USER = id,
                        ACTIVE = true

                    });
                }
            }
            if (i > 0)
            {
                return Json(new { success = true, message = "The products have been imported Succesfully", Title = "The products have been imported Succesfully!", CssClassName = "alert alert-" + (flag ? "success" : "info") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, message = "", Title = "Oops!, Looks like there was no items to be added", CssClassName = "alert alert-" + (flag ? "success" : "warning") + " alert-dismissible" }, JsonRequestBehavior.AllowGet);

            }

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

    #region JSON 

    //public JsonResult GetReferences(string term)
    //{
    //    List<CODIGO_ARTICULO> lista = db.CODIGO_ARTICULO.ToList();

    //    return Json(lista.Where(o => o.ARTICULO.Contains(term) || o.CODIGO.Contains(term)).Select(o => new { value = o.ID_CODIGO_ARTICULO, label = (o.CODIGO + "-" + o.ARTICULO) }), JsonRequestBehavior.AllowGet);
    //}

    //[HttpGet]
    //public JsonResult GetReferencePrices(int id)
    //{
    //    CODIGO_ARTICULO x = db.CODIGO_ARTICULO.Where(o => o.ID_CODIGO_ARTICULO == id).FirstOrDefault();

    //    return Json(new { official_fee = x.FEE, professional_fee = x.TAX }, JsonRequestBehavior.AllowGet);
    //}
    #endregion

}
