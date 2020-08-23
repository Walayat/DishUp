using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DishUp;
using DishUp.Models;
using Microsoft.AspNet.Identity;

namespace DishUp.Controllers
{
    public class RECIPEsController : Controller
    {
        private dishupEntities db = new dishupEntities();

        // GET: RECIPEs
        public ActionResult Index()
        {
            var rECIPEs = db.RECIPEs.Include(r => r.MEDIDA).Include(r => r.MEDIDA1).Include(r => r.CATEGORY).Include(r => r.CATEGORY1).Include(r => r.CATEGORY2).Include(r => r.CATEGORY3);
            return View(rECIPEs.ToList());
        }

        // GET: RECIPEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECIPE rECIPE = db.RECIPEs.Find(id);
            if (rECIPE == null)
            {
                return HttpNotFound();
            }
            return View(rECIPE);
        }

        // GET: RECIPEs/Create
        public ActionResult Create()
        {
            ViewBag.ID_MEDIDA_YIELD = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            ViewBag.ID_MEDIDA_PORTION = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            ViewBag.ID_CATEGORY = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME");
            ViewBag.ID_COOK_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME");
            ViewBag.ID_PREP_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME");
            ViewBag.ID_SHELF_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME");
            return View();
        }

        // POST: RECIPEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_RECIPE,ID_CATEGORY,ID_MEDIDA_YIELD,ID_MEDIDA_PORTION,ID_PREP_TIME,ID_COOK_TIME,ID_SHELF_TIME,YIELD,PORTION_SIZE,PORTION_NUMBER,PRICE,PREP_TIME,COOK_TIME,SHELF_LIFE,IMAGEN,NAME,ACTIVE")] RECIPE rECIPE)
        {
            if (ModelState.IsValid)
            {
                db.RECIPEs.Add(rECIPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_MEDIDA_YIELD = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", rECIPE.ID_MEDIDA_YIELD);
            ViewBag.ID_MEDIDA_PORTION = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", rECIPE.ID_MEDIDA_PORTION);
            ViewBag.ID_CATEGORY = new SelectList(db.CATEGORies, "ID_CATEGORY", "ID_CATEGORY", rECIPE.ID_CATEGORY);
            ViewBag.ID_COOK_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "ID_CATEGORY", rECIPE.ID_COOK_TIME);
            ViewBag.ID_PREP_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "ID_CATEGORY", rECIPE.ID_PREP_TIME);
            ViewBag.ID_SHELF_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "ID_CATEGORY", rECIPE.ID_SHELF_TIME);
            return View(rECIPE);
        }

        // GET: RECIPEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECIPE rECIPE = db.RECIPEs.Find(id);
            if (rECIPE == null)
            {
                return HttpNotFound();
            }
            var idUser = User.Identity.GetUserId();
            ViewBag.ID_USER_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER == idUser && x.ACTIVE == true).ToList(), "ID_USER_SUPPLIER_INSUMO", "NAME");
            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            ViewBag.ID_MEDIDA_YIELD = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", rECIPE.ID_MEDIDA_YIELD);
            ViewBag.ID_MEDIDA_PORTION = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", rECIPE.ID_MEDIDA_PORTION);
            ViewBag.ID_CATEGORY = new SelectList(db.CATEGORies.Where(x => x.ID_CATEGORY == 4), "ID_CATEGORY", "NAME", rECIPE.ID_CATEGORY);
            ViewBag.ID_COOK_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME", rECIPE.ID_COOK_TIME);
            ViewBag.ID_PREP_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME", rECIPE.ID_PREP_TIME);
            ViewBag.ID_SHELF_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME", rECIPE.ID_SHELF_TIME);
            return View(rECIPE);
        }

        // POST: RECIPEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_RECIPE,ID_CATEGORY,ID_MEDIDA_YIELD,ID_MEDIDA_PORTION,ID_PREP_TIME,ID_COOK_TIME,ID_SHELF_TIME,YIELD,PORTION_SIZE,PORTION_NUMBER,PRICE,PREP_TIME,COOK_TIME,SHELF_LIFE,IMAGEN,NAME,ACTIVE")] RECIPE rECIPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECIPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var idUser = User.Identity.GetUserId();
            ViewBag.ID_USER_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER == idUser && x.ACTIVE == true).ToList(), "ID_USER_SUPPLIER_INSUMO", "NAME");

            ViewBag.ID_MEDIDA_YIELD = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", rECIPE.ID_MEDIDA_YIELD);
            ViewBag.ID_MEDIDA_PORTION = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE", rECIPE.ID_MEDIDA_PORTION);
            ViewBag.ID_CATEGORY = new SelectList(db.CATEGORies.Where(x => x.ID_CATEGORY == 4), "ID_CATEGORY", "NAME", rECIPE.ID_CATEGORY);
            
            ViewBag.ID_COOK_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME", rECIPE.ID_COOK_TIME);
            ViewBag.ID_PREP_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME", rECIPE.ID_PREP_TIME);
            ViewBag.ID_SHELF_TIME = new SelectList(db.CATEGORies, "ID_CATEGORY", "NAME", rECIPE.ID_SHELF_TIME);
            return View(rECIPE);
        }

        // GET: RECIPEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECIPE rECIPE = db.RECIPEs.Find(id);
            if (rECIPE == null)
            {
                return HttpNotFound();
            }
            return View(rECIPE);
        }

        // POST: RECIPEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RECIPE rECIPE = db.RECIPEs.Find(id);
            db.RECIPEs.Remove(rECIPE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult GetRecipeDetails(int id)
        {
            var idUser = User.Identity.GetUserId();
            ViewBag.ID_USER_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER == idUser && x.ACTIVE == true).ToList(), "ID_USER_SUPPLIER_INSUMO", "NAME");
            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");


            RecipeDetailViewModel rcd = new RecipeDetailViewModel
            {
                ID_RECIPE = id,
                details = db.RECIPE_DETAIL.Where(x => x.ID_RECIPE == id && x.ACTIVE == true)
            };
            return PartialView("_recipeDetails", rcd);

        }

        [HttpPost]
        public ActionResult AddIngredient(int? ID_USER_SUPPLIER_INSUMO, int? QUANTITY, int? ID_RECIPE, int? ID_PORTIONED_BY)
        {
            var idUser = User.Identity.GetUserId();
            ViewBag.ID_USER_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER == idUser && x.ACTIVE == true).ToList(), "ID_USER_SUPPLIER_INSUMO", "NAME");
            ViewBag.ID_PORTIONED_BY = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            USER_SUPPLIER_INSUMO usi = db.USER_SUPPLIER_INSUMO.Find(ID_USER_SUPPLIER_INSUMO);

            decimal costoPorUnidad = Convert.ToDecimal(usi.INDIVIDUAL_PRICE);
            decimal quantity = Convert.ToDecimal(QUANTITY);
            var tipoUnidad = usi.MEDIDA.NOMBRE;
            decimal costo = 0;
            decimal unitRequired = 0;

            switch (ID_PORTIONED_BY)
            {
                case 1: //unit
                    costo = quantity * costoPorUnidad;

                    break;
                case 2: //ounce
                    switch (tipoUnidad)
                    {
                        case "Ounce":
                            costo = quantity * costoPorUnidad;
                            break;
                        case "Ml":
                            decimal cantMl = quantity * 29;
                            costo = ( costoPorUnidad * cantMl);
                            break;
                        case "Liter":
                            decimal cantMLL= quantity * 29;
                            decimal costoporMl = costoPorUnidad / 1000;
                            costo = cantMLL * costoporMl;
                            break;
                        case "Grams":
                            decimal cantGr = quantity * 29;
                            costo = (costoPorUnidad * cantGr);
                            break;
                        case "Kilo":
                            decimal cantGRR = quantity * 29;
                            decimal costoporGr = costoPorUnidad / 1000;
                            costo = cantGRR * costoporGr;
                            break;
                    }
                    break;
                case 3: //ml 
                    switch (tipoUnidad)
                    {
                        case "Ounce":
                            decimal cantOz = Convert.ToDecimal(QUANTITY / 29);
                            costo = cantOz * costoPorUnidad;
                            break;
                        case "Ml":
                            costo = (quantity * costoPorUnidad);
                            break;
                        case "Liter":
                            decimal costoporMl = costoPorUnidad  / 1000;
                            costo = quantity * costoporMl;
                            break;
                        case "Grams":
                            costo = (quantity * costoPorUnidad);
                            break;
                        case "Kilo":
                            decimal costoporGr = costoPorUnidad / 1000;
                            costo = quantity * costoporGr;
                            break;

                    }

                    break;

                case 4: //Liter 

                    switch (tipoUnidad)
                    {
                        case "Ounce":
                            decimal cantMl = quantity * 1000;
                            decimal cantOz = Convert.ToDecimal(cantMl / 29);
                            costo = cantOz * costoPorUnidad;
                            break;
                        case "Ml":
                            decimal costoPorLitro =  costoPorUnidad * 1000;
                            costo = (quantity * costoPorLitro);
                            break;
                        case "Liter":
                            costo = quantity * costoPorUnidad;
                            break;
                        case "Grams":
                            decimal costoPorGr = costoPorUnidad * 1000;
                            costo = (quantity * costoPorGr);
                            break;
                        case "Kilo":
                            costo = quantity * costoPorUnidad;
                            break;
                    }
                    break;

                case 5: //grams
                    switch (tipoUnidad)
                    {
                        case "Ounce":
                            decimal cantOz = Convert.ToDecimal(QUANTITY / 29);
                            costo = cantOz * costoPorUnidad;
                            break;
                        case "Ml":
                            costo = (quantity * costoPorUnidad);
                            break;
                        case "Liter":
                            decimal costoporMl = costoPorUnidad / 1000;
                            costo = quantity * costoporMl;
                            break;
                        case "Grams":
                            costo = (quantity * costoPorUnidad);
                            break;
                        case "Kilo":
                            decimal costoporGr = costoPorUnidad / 1000;
                            costo = quantity * costoporGr;
                            break;
                    }
                    break;

                case 6: //Kilograms

                    switch (tipoUnidad)
                    {
                        case "Ounce":
                            decimal cantMl = quantity * 1000;
                            decimal cantOz = Convert.ToDecimal(cantMl / 29);
                            costo = cantOz * costoPorUnidad;
                            break;
                        case "Ml":
                            decimal costoPorLitro = costoPorUnidad * 1000;
                            costo = (quantity * costoPorLitro);
                            break;
                        case "Liter":
                            costo = quantity * costoPorUnidad;
                            break;
                        case "Grams":
                            decimal costoPorGr = costoPorUnidad * 1000;
                            costo = (quantity * costoPorGr);
                            break;
                        case "Kilo":
                            costo = quantity * costoPorUnidad;
                            break;
                    }
                    break;


            }

            RECIPE_DETAIL rd = new RECIPE_DETAIL
            {
                COST = costo,
                COST_PERCENTAGE = 1,
                ID_RECIPE = ID_RECIPE,
                ID_USER_SUPPLIER_INSUMO = ID_USER_SUPPLIER_INSUMO,
                QUANTITY = QUANTITY,
                ID_PORTIONED_BY = ID_PORTIONED_BY,
                ACTIVE = true

            };
            db.RECIPE_DETAIL.Add(rd);
            db.SaveChanges();

            RecipeDetailViewModel rcd = new RecipeDetailViewModel
            {
                ID_RECIPE = Convert.ToInt32(ID_RECIPE),
                details = db.RECIPE_DETAIL.Where(x => x.ID_RECIPE == ID_RECIPE && x.ACTIVE == true)
            };

            return PartialView("_recipeDetails", rcd);

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
