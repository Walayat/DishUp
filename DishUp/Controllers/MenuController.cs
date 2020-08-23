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
using DishUp.Filters;
using DishUp.Models;

namespace DishUp.Controllers
{
    [NotificationFilter]
    public class MenuController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        #region ABM

        // GET: Menu
        public ActionResult Index()
        {
            return View(db.MENUs.Where(x => x.ACTIVE == true). ToList());
        }

        // GET: Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_MENU,NOMBRE,ACTIVE")] MENU mENU)
        {
            if (ModelState.IsValid)
            {
                int? ID;
                using (var database  = new dishupEntities())
                {
                    mENU.ACTIVE = true;
                    database.MENUs.Add(mENU);
                    database.SaveChanges();
                    ID = mENU.ID_MENU;
                }
                db.MENUs.Add(mENU);
                db.SaveChanges();
                return RedirectToAction("Edit",new { id = ID });
            }

            return View(mENU);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_MENU,NOMBRE,ACTIVE")] MENU mENU,double COST)
        {
            if (ModelState.IsValid)
            {
                mENU.ACTIVE = true;
                mENU.COST = Convert.ToDecimal(COST);
                db.Entry(mENU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mENU);
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENUs.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MENU mENU = db.MENUs.Find(id);
            mENU.ACTIVE = false;
            db.Entry(mENU).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region SUPPLIES

        public ActionResult GetSuppliesList(int id)
        {
            IEnumerable<MENU_INSUMO> listaInsumo = db.MENU_INSUMO.Include(x=> x.INSUMO).Where(x => x.ID_MENU == id && x.ACTIVE == 1).ToList();
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            return PartialView("_menuInsumos", listaInsumo);

        }

        public ActionResult EditMenuSupplie (int ID)
        {
            MENU_INSUMO menuInsumo = db.MENU_INSUMO.Find(ID);

            if (menuInsumo != null)
            {
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE",menuInsumo.ID_MEDIDA);
                ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE",menuInsumo.ID_INSUMO);
                return PartialView("_addSupplie",menuInsumo);

            }else
            {
                ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "ame");

                return PartialView("_addSupplie",new MENU_INSUMO());

            }


        }
        public ActionResult DeleteMenuSupplie(int id, int ID_MENU)
        {
            MENU_INSUMO m = db.MENU_INSUMO.Find(id);
            m.ACTIVE = 0;
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();

            IEnumerable<MENU_INSUMO> listaProductos = db.MENU_INSUMO.Include(x => x.INSUMO).Where(x => x.ID_MENU == ID_MENU && x.ACTIVE == 1).ToList();

            return PartialView("_menuInsumos",listaProductos);
        }
        [HttpGet]
        public ActionResult NewSupplie(int id)
        {
            MENU_INSUMO pr = new MENU_INSUMO();
            pr.ID_MENU = id;
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");
            return PartialView("_addSupplie", new MENU_INSUMO { ID_MENU = id });
        }

        public ActionResult GetPackageCost(int ID_MENU)
        {
            MenuCostViewModel mcvm = GetProductCost(ID_MENU);
            return PartialView("_packageCost",mcvm);

        }
        [HttpPost]
        public ActionResult AddSupplies(int? ID_INSUMO, int? QUANTITY, int ID_MENU)
        {
            if (ID_INSUMO == 0)
            {
                ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");

                return PartialView("_addSupplie", new MENU_INSUMO { ID_MENU = ID_MENU });

            }
            MENU_INSUMO menuInsumo = db.MENU_INSUMO.Include(x=> x.INSUMO).Where(x => x.ID_MENU == ID_MENU && x.ID_INSUMO == ID_INSUMO && x.ACTIVE == 1).FirstOrDefault();

            if (menuInsumo != null)
            {
                menuInsumo.QUANTITY = QUANTITY;
                db.Entry<MENU_INSUMO>(menuInsumo).State = EntityState.Modified;

            }
            else
            {
                MENU_INSUMO PR = new MENU_INSUMO
                {
                    ID_MENU = ID_MENU,
                    QUANTITY = QUANTITY,
                    ID_INSUMO = ID_INSUMO,
                    ACTIVE = 1

                };
                db.MENU_INSUMO.Add(PR);
            }
            db.SaveChanges();
            //IEnumerable<MENU_INSUMO> listaInsumos = db.MENU_INSUMO.Where(x => x.ID_MENU == ID_MENU && x.ACTIVE == true).ToList();
            IEnumerable<MENU_INSUMO> listaInsumos = db.MENU_INSUMO.Include(x=> x.INSUMO).Where(x => x.ID_MENU == ID_MENU && x.ACTIVE == 1).ToList();
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");

            return PartialView("_menuInsumos", listaInsumos);
        }

        #endregion

        #region PRODUCTS

        
        public ActionResult GetProductList(int id)
        {
           
            return PartialView("_menuProductos", db.MENU_PRODUCTO.Include(x=>x.PRODUCTO).Where(x=>x.ID_MENU == id && x.ACTIVE == true).ToList());

        }
        public ActionResult EditProductMenu(int ID)
        {
            MENU_PRODUCTO menuProducto = db.MENU_PRODUCTO.Find(ID);

            if (menuProducto != null)
            {
                ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes.Where(x=>x.ACTIVE == true), "ID_PRODUCTO", "NOMBRE", menuProducto.ID_PRODUCTO);
                return PartialView("_addProduct", menuProducto);

            }
            else
            {
                ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes.Where(x => x.ACTIVE == true), "ID_PRODUCTO", "NOMBRE");

                return PartialView("_addProduct", new MENU_PRODUCTO());

            }


        }
        public ActionResult DeleteMenuProduct(int id, int ID_MENU)
        {
            MENU_PRODUCTO m = db.MENU_PRODUCTO.Find(id);
            m.ACTIVE = false;
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();

            IEnumerable<MENU_PRODUCTO> listaProductos = db.MENU_PRODUCTO.Include(x => x.PRODUCTO).Where(x => x.ID_MENU == ID_MENU && x.ACTIVE == true).ToList();

            return PartialView("_menuProductos",listaProductos);
        }
        public ActionResult AddProduct(int? ID)
        {
            if (ID == 0)
            {
                ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes.Where(x => x.ACTIVE == true), "ID_PRODUCTO", "NOMBRE");
                ModelState.Clear();
                return PartialView("_addProduct", new MENU_PRODUCTO { ID_PRODUCTO = ID });

            }
          
            IEnumerable<MENU_PRODUCTO> listaProductos = db.MENU_PRODUCTO.Include(x=>x.PRODUCTO).Where(x => x.ID_MENU == ID && x.ACTIVE == true).ToList();
            ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes, "ID_PRODUCTO", "NOMBRE");

            return PartialView("_addProduct", new MENU_PRODUCTO { ID_MENU= ID });
        }
        
        [HttpPost]
        public ActionResult AddProducts(int? ID_PRODUCTO, int? CANTIDAD, int? ID_MENU, int ID_MENU_PRODUCTO)
        {
            if (ID_PRODUCTO == 0)
            {
                ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes.Where(x=>x.ACTIVE ==true), "ID_PRODUCTO", "NOMBRE");
                ModelState.Clear();
                return PartialView("_addProduct", new MENU_PRODUCTO { ID_PRODUCTO = ID_PRODUCTO });

            }

            MENU_PRODUCTO menuPRODUCTO = db.MENU_PRODUCTO.Where(x => x.ID_MENU == ID_MENU && x.ID_PRODUCTO == ID_PRODUCTO && x.ACTIVE == true).FirstOrDefault();

            if (menuPRODUCTO != null)
            {
                menuPRODUCTO.CANTIDAD = CANTIDAD;
                menuPRODUCTO.ID_PRODUCTO = ID_PRODUCTO;
                menuPRODUCTO.ACTIVE = true;
                db.Entry<MENU_PRODUCTO>(menuPRODUCTO).State = EntityState.Modified;

            }
            else
            {
                MENU_PRODUCTO PR = new MENU_PRODUCTO
                {
                    ID_MENU = ID_MENU,
                    CANTIDAD = CANTIDAD,
                    ID_PRODUCTO = ID_PRODUCTO,
                    ACTIVE = true

                };
                db.MENU_PRODUCTO.Add(PR);
            }
            db.SaveChanges();
            //IEnumerable<MENU_PRODUCTO> listaPRODUCTOs = db.MENU_PRODUCTO.Where(x => x.ID_MENU == ID_MENU && x.ACTIVE == true).ToList();
            IEnumerable<MENU_PRODUCTO> listaProductos = db.MENU_PRODUCTO.Include(x => x.PRODUCTO).Where(x => x.ID_MENU == ID_MENU && x.ACTIVE==true).ToList();
            ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes.Where(x => x.ACTIVE == true), "ID_PRODUCTO", "NOMBRE");

            return PartialView("_menuProductos", listaProductos);
        }

        #endregion

        #region JSONLIST


        [HttpGet]
        public JsonResult GetSupplie(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "SUPPLIE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProduct(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "PRODUCT");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region COSTING
        public MenuCostViewModel GetProductCost(int? ID_MENU)
        {
        
            IEnumerable<MENU_INSUMO> listaInsumos = db.MENU_INSUMO.Include(x => x.INSUMO).Include(x => x.MEDIDA).Where(x => x.ID_MENU == ID_MENU).ToList();
            IEnumerable<MENU_PRODUCTO> listaProductos= db.MENU_PRODUCTO.Where(x => x.ID_MENU == ID_MENU).ToList();


            
      Decimal? costoTotal = 0;
            Decimal? costoTotalProductos= 0;
            foreach (MENU_INSUMO prod in listaInsumos)
            {
                //TRAIGO DETALLE DEL INSUMO
                int idInsumo = prod.ID_INSUMO.Value;
                INSUMO ins = db.INSUMOes.Where(x => x.ID_INSUMO == idInsumo).FirstOrDefault();
                decimal? costo = 0;

                if (ins.ID_MEDIDA == 1) //KILO
                {
                    if (prod.ID_MEDIDA == 1) //KILO
                    {
                        costo = ins.WHOLESALE_PRICE * Convert.ToDecimal(prod.QUANTITY);

                    }
                    if (prod.ID_MEDIDA == 2) // GRAMOS
                    {
                        decimal? costoperGR = ins.WHOLESALE_PRICE / 1000;
                        costo = costoperGR * prod.QUANTITY;
                    }
                }

                if (ins.ID_MEDIDA == 5) //LITRO
                {
                    if (prod.ID_MEDIDA == 5) //LITRO
                    {
                        costo = ins.WHOLESALE_PRICE * Convert.ToDecimal(prod.QUANTITY);

                    }
                    if (prod.ID_MEDIDA == 6) // ML
                    {
                        decimal? costoperGR = ins.WHOLESALE_PRICE / 1000;
                        costo = costoperGR * prod.QUANTITY;
                    }
                }
                if (ins.ID_MEDIDA == 3) //PACKAGE
                {
                    costo = ins.PRICE_PER_UNIT * prod.QUANTITY;
                }
                if (ins.ID_MEDIDA == 7) //UNIT
                {
                    costo = ins.PRICE_PER_UNIT * prod.QUANTITY;
                }
                costoTotal += costo;

            }
            foreach (MENU_PRODUCTO prod in listaProductos)
            {
                if (costoTotalProductos == 0)
                {
                    costoTotalProductos = prod.PRODUCTO.WHOLESALE_PRICE * prod.CANTIDAD;
                }
                else
                {
                    costoTotalProductos += (prod.CANTIDAD * prod.PRODUCTO.WHOLESALE_PRICE);

                }
            }

            return new MenuCostViewModel { TotalPackage = costoTotal + costoTotalProductos };
        }

        #endregion
        public ActionResult UploadFile(FineUpload upload, string id)
        {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader
            //  return View();

            var dir = Server.MapPath("~/UploadPictures/");
            var filePath = Path.Combine(dir, upload.Filename);

            try
            {
                upload.SaveAs(filePath);
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

            //// the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = 12345 });
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
