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
using DishUp.Models;

namespace DishUp.Controllers
{
    public class ProductController : BaseController
    {
        private dishupEntities db = new dishupEntities();


        #region PRODUCT 
        // GET: Producto
        public ActionResult Index()
        {
            return View(db.PRODUCTOes.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTOes.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTO);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRODUCTO,NOMBRE,ES_COMBO,ACTIVE,WHOLESALE_PRICE,SALE_PRICE,PESO_UNIDADES")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTOes.Add(pRODUCTO);
                db.SaveChanges();
                return RedirectToAction("Edit","Product",new { id=pRODUCTO.ID_PRODUCTO });
            }

            return View(pRODUCTO);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTOes.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO, "ID_SUPPLIER_INSUMO", "NAME");

            return View(pRODUCTO);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRODUCTO,NOMBRE,ES_COMBO,ACTIVE,WHOLESALE_PRICE,SALE_PRICE,PESO_UNIDADES")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pRODUCTO);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTOes.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTO);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTO pRODUCTO = db.PRODUCTOes.Find(id);
            db.PRODUCTOes.Remove(pRODUCTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion



        [HttpGet]
        public ActionResult AddSupplie(int id)
        {
            PRODUCTO_INSUMO pr = new PRODUCTO_INSUMO();
            pr.ID_PRODUCTO = id;
            ViewBag.ID_USER_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO, "ID_USER_SUPPLIER_INSUMO", "NAME");
            ViewBag.ID_MEDIDA = new SelectList(db.MEDIDAs, "ID_MEDIDA", "NOMBRE");

            return PartialView("_addSupplie",pr);
        }
        [HttpPost]
        public ActionResult AddSuppliess(int? ID_USER_SUPPLIER_INSUMO, int? quantity, int? ID_PRODUCTO, int? ID_MEDIDA)
        {
            var existingProduct = db.PRODUCTO_INSUMO.Where(x => x.ID_PRODUCTO == ID_PRODUCTO && x.ID_USER_SUPPLIER_INSUMO == ID_USER_SUPPLIER_INSUMO && x.ACTIVE == true).FirstOrDefault();
            if (existingProduct != null)
            {
                existingProduct.ID_PRODUCTO = ID_PRODUCTO;
                existingProduct.quantity = quantity;
                existingProduct.ID_MEDIDA = ID_MEDIDA;
                db.Entry(existingProduct).State = EntityState.Modified;

            }
            else
            {
                PRODUCTO_INSUMO PR = new PRODUCTO_INSUMO
                {
                    ID_USER_SUPPLIER_INSUMO = ID_USER_SUPPLIER_INSUMO,
                    quantity = quantity,
                    ID_PRODUCTO = ID_PRODUCTO,
                    ID_MEDIDA = ID_MEDIDA,
                    ACTIVE = true
                };
                db.PRODUCTO_INSUMO.Add(PR);
            }

            db.SaveChanges();
            supplieListViewModel slvm = GetProductListCost(ID_PRODUCTO,quantity);

            return PartialView("_suppliesList", slvm);

        }
        [HttpPost]
        public ActionResult EditSupplie(PRODUCTO_INSUMO PR)
        {
            PRODUCTO_INSUMO prod = db.PRODUCTO_INSUMO.Find(PR.ID_PRODUCTO_INSUMO);
            prod.ID_USER_SUPPLIER_INSUMO = PR.ID_USER_SUPPLIER_INSUMO;
            prod.quantity = PR.quantity;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();
            var listaInsumos = db.PRODUCTO_INSUMO.Where(x => x.ID_PRODUCTO == PR.ID_PRODUCTO).ToList();
            supplieListViewModel slvm = GetProductListCost(PR.ID_PRODUCTO, PR.quantity);

            return PartialView("_suppliesList", slvm);
            //return PartialView("_suppliesList", listaInsumos);
        }



        public supplieListViewModel GetProductListCost(int? ID_PRODUCTO, int? quantity)
        {
            supplieListViewModel listaInsumoVM = new supplieListViewModel
            {
                listaInsumos = db.PRODUCTO_INSUMO.Include(x => x.PRODUCTO).Include(x => x.MEDIDA).Where(x => x.ID_PRODUCTO == ID_PRODUCTO && x.ACTIVE == true).ToList()
            };

            listaInsumoVM.costoTotal = 0;
            foreach (PRODUCTO_INSUMO prod in listaInsumoVM.listaInsumos)
            {
                //TRAIGO DETALLE DEL INSUMO
                int idInsumo = prod.ID_USER_SUPPLIER_INSUMO.Value;
                USER_SUPPLIER_INSUMO ins = db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER_SUPPLIER_INSUMO== idInsumo).FirstOrDefault();
                decimal? costo = 0;

                if (ins.ID_MEDIDA_PACKAGE == 6) //KILO
                {
                    if (prod.quantity is null)
                    {
                        costo = ins.INDIVIDUAL_PRICE;
                    }
                    else
                    {
                        costo = ins.INDIVIDUAL_PRICE * Convert.ToDecimal(prod.quantity);
                    }
                }

                else if (ins.ID_MEDIDA_PACKAGE == 5) // GRAMOS
                {
                    decimal? costoperGR = ins.PRICE / 1000;
                    if (!(prod.quantity is null))
                    {
                        costo = costoperGR * prod.quantity;
                    }
                }

                else if (ins.ID_MEDIDA_PACKAGE == 4) //LITRO
                {
                    if (!(prod.quantity is null))
                    {
                        costo = ins.PRICE * Convert.ToDecimal(prod.quantity);
                    }
                    else
                    {
                        costo = ins.PRICE;
                    }
                }

                else if (ins.ID_MEDIDA_PACKAGE == 3) //ML
                {
                    decimal? costoperGR = ins.PRICE / 1000;
                    costo = costoperGR * prod.quantity;
                }

                else if (ins.ID_MEDIDA_PACKAGE == 1) //UNIT
                {
                    costo = ins.INDIVIDUAL_PRICE * prod.quantity;
                }

                
                listaInsumoVM.costoTotal += costo;

            }
            return listaInsumoVM;
        }
        public supplieListViewModel GetProductCost(int? ID_PRODUCTO)
        {
            supplieListViewModel listaInsumoVM = new supplieListViewModel
            {
                listaInsumos = db.PRODUCTO_INSUMO.Include(x => x.USER_SUPPLIER_INSUMO).Include(x => x.MEDIDA).Where(x => x.ID_PRODUCTO == ID_PRODUCTO && x.ACTIVE == true).ToList()
            };

            listaInsumoVM.costoTotal = 0;
            foreach (PRODUCTO_INSUMO prod in listaInsumoVM.listaInsumos)
            {
                //TRAIGO DETALLE DEL INSUMO
                int idInsumo = prod.ID_USER_SUPPLIER_INSUMO.Value;
                USER_SUPPLIER_INSUMO ins = db.USER_SUPPLIER_INSUMO.Where(x => x.ID_USER_SUPPLIER_INSUMO== idInsumo).FirstOrDefault();
                decimal? costo = 0;

                if (ins.ID_MEDIDA_PACKAGE == 6) //KILO
                {
                    costo = ins.INDIVIDUAL_PRICE * Convert.ToDecimal(prod.quantity);
                }

                else if (ins.ID_MEDIDA_PACKAGE == 5) // GRAMOS
                {
                    decimal? costoperGR = ins.PRICE / 1000;
                    costo = costoperGR * prod.quantity;
                }

                else if (ins.ID_MEDIDA_PACKAGE == 4) //LITRO
                {
                    costo = ins.PRICE * Convert.ToDecimal(prod.quantity);
                }

                else if (ins.ID_MEDIDA_PACKAGE == 3) //ML
                {
                    decimal? costoperGR = ins.PRICE / 1000;
                    costo = costoperGR * prod.quantity;
                }

                else if (ins.ID_MEDIDA_PACKAGE == 1) //UNIT
                {
                    costo = ins.INDIVIDUAL_PRICE * prod.quantity;
                }

                listaInsumoVM.costoTotal += costo;

            }
            return listaInsumoVM;
        }

        [HttpPost]
        public ActionResult GetProfit(int ID_PRODUCTO,int profitpercentaje = 0) {
            decimal? result = 0;
            supplieListViewModel slvm = GetProductCost(ID_PRODUCTO);
            result = slvm.costoTotal;
            if (profitpercentaje == 0)
            {
                return PartialView("_profitResult", result);

            }else
            {
                result = result + ((result * profitpercentaje) / 100);

                return PartialView("_profitResult", result);

            }

        }
        [HttpGet]
        public ActionResult GetProfit(int ID_PRODUCTO)
        {
            decimal? result = 0;
            supplieListViewModel slvm = GetProductCost(ID_PRODUCTO);
            result = slvm.costoTotal;
            ViewBag.ID_INSUMO = new SelectList(db.PRODUCTOes, "ID_PRODUCTO", "NOMBRE", ID_PRODUCTO);
            ProfitCalculator profCalc = new ProfitCalculator
            {
                ID_PRODUCTO = ID_PRODUCTO,
                profitpercentaje = 0
            };
            return PartialView("_profitCalculator", profCalc);


        }
        public ActionResult GetSuppliesList(int id)
        {
            supplieListViewModel slvm = new supplieListViewModel
            {
                listaInsumos = db.PRODUCTO_INSUMO.Include(x => x.MEDIDA).Where(x => x.ID_PRODUCTO == id && x.ACTIVE == true).ToList(),
                costoTotal = 0
            };
            slvm = GetProductCost(id);
            ViewBag.ID_PRODUCTO= new SelectList(db.PRODUCTOes, "ID_PRODUCTO", "NOMBRE");

            return PartialView("_suppliesList", slvm);

        }
  
        public ActionResult EditProductSupplie (int id,int idInsumo)
        {
            PRODUCTO_INSUMO pr = db.PRODUCTO_INSUMO.Find(id);
            ViewBag.ID_USER_SUPPLIER_INSUMO = new SelectList(db.USER_SUPPLIER_INSUMO, "ID_USER_SUPPLIER_INSUMO", "NAME",idInsumo);

            return PartialView("_editSupplie", pr);
        }

        public ActionResult DeleteProductSupplie (int id)
        {
            var ps = db.PRODUCTO_INSUMO.Find(id);
            ps.ACTIVE = false;
            db.Entry(ps).State = EntityState.Modified;
            db.SaveChanges();
            IEnumerable<PRODUCTO_INSUMO> listaInsumos = db.PRODUCTO_INSUMO.Include(x=> x.USER_SUPPLIER_INSUMO).Include(x=>x.PRODUCTO).Where(x => x.ID_PRODUCTO == ps.ID_PRODUCTO && x.ACTIVE == true).ToList();
            Success("Ingredient Removed.");

            supplieListViewModel slvm = GetProductListCost(ps.ID_PRODUCTO, ps.quantity);
            return PartialView("_suppliesList", slvm);
        }

        public ActionResult GetImage (int ID)
        {
            String imgSource = db.PRODUCTOes.Where(x => x.ID_PRODUCTO == ID).Select(x=>x.IMAGEN).First();
            return PartialView("_Uploader", imgSource);
        }
        public ActionResult UploadFile(FineUpload upload, int? id)
        {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader
            //  return View();
            PRODUCTO pr = db.PRODUCTOes.Where(x=> x.ID_PRODUCTO == id).First();
            
            var dir = Server.MapPath("~/UploadPictures/");
            var filePath = Path.Combine(dir, upload.Filename);
            pr.IMAGEN = "../../UploadPictures/" + upload.Filename;
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
            return new FineUploaderResult(true, new { extraInformation = pr.IMAGEN });
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
