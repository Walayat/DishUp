using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DishUp;

namespace DishUp.Controllers
{
    public class OrdersController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var ORDER = db.ORDERS.Include(o => o.SUPPLIER);
            return View(ORDER.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER ORDER = db.ORDERS.Find(id);
            if (ORDER == null)
            {
                return HttpNotFound();
            }
            return View(ORDER);
        }

        // GET: Orders/Create
        public ActionResult Create(int id)
        {
            ORDER ord = new ORDER
            {
                ID_SUPPLIER = id,
                IS_DRAFT= true

            };
            using (var cont = new dishupEntities())
            {
                cont.ORDERS.Add(ord);
                cont.SaveChanges();
            }

            ViewBag.ID_SUPPLIER_INSUMO = new SelectList(db.SUPPLIER_INSUMO.Where(m => m.ID_SUPPLIER == id).Select(x => new SelectListItem { Value = x.ID_SUPPLIER_INSUMO.ToString(), Text = x.INSUMO.NOMBRE }),"Value","Text").ToList();
            ViewBag.ID_SUPPLIER = new SelectList(db.SUPPLIERs, "ID_SUPPLIER", "NAME");
            return View(ord);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ORDER,ID_SUPPLIER,ORDER_DATE,DELIVERED,IS_DRAFT,ACTIVE")] ORDER ORDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ORDER).State = EntityState.Modified;
                db.SaveChanges();
                SendEmail("federicogbatista.au@gmail.com", ORDER.ID_ORDER);
                return RedirectToAction("Index");
            }

            MailManager mm = new MailManager();
           

            ViewBag.ID_SUPPLIER = new SelectList(db.SUPPLIERs, "ID_SUPPLIER", "NAME", ORDER.ID_SUPPLIER);
            return View(ORDER);
        }
        public void SendEmail(string email, int ID_ORDER)
        {
            MailManager mm = new MailManager();
            StreamReader str = new StreamReader(Server.MapPath("~/Templates/NewOrder.html"));

            var body = str.ReadToEnd();

            List<ORDERS_ITEM> suppliesList = db.ORDERS_ITEM.Include(x=>x.SUPPLIER_INSUMO).Include(x=>x.ORDER).Where(x => x.ID_ORDER == ID_ORDER).ToList();

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("<tr><th>CODE</th><th>DESCRIPTION</th><th>QUANTITY</th></tr>");
            foreach (ORDERS_ITEM ci in suppliesList)
            {
                strBuilder.AppendLine("<tr>");

                strBuilder.AppendLine("<td width='100'>");
                strBuilder.AppendLine(ci.SUPPLIER_INSUMO.INSUMO.NOMBRE);
                strBuilder.AppendLine("<td/>");

                strBuilder.AppendLine("<td align='left' width='500'>");
                strBuilder.AppendLine(ci.SUPPLIER_INSUMO.INSUMO.NOMBRE);
                strBuilder.AppendLine("<td/>");

                strBuilder.AppendLine("<td width='100'>");
                strBuilder.AppendLine(ci.QUANTITY.ToString());
                strBuilder.AppendLine("<td/>");

                strBuilder.AppendLine("</tr>");

            }
            body = body.Replace("#body#", strBuilder.ToString());
            mm.EnviarMail("Order from Pablos Kitchen", body, "federicogbatista.au@gmail.com");
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER ORDER = db.ORDERS.Find(id);
            if (ORDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_SUPPLIER = new SelectList(db.SUPPLIERs, "ID_SUPPLIER", "NAME", ORDER.ID_SUPPLIER);
            return View(ORDER);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ORDER,ID_SUPPLIER,ORDER_DATE,DELIVERED,IS_DRAFT,ACTIVE")] ORDER ORDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ORDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_SUPPLIER = new SelectList(db.SUPPLIERs, "ID_SUPPLIER", "NAME", ORDER.ID_SUPPLIER);
            return View(ORDER);
        }
        public ActionResult DeleteSupplie(int id,int ID_ORDER)
        {
            var ps = db.ORDERS_ITEM.Find(id);
            ps.ACTIVE = 0;
            db.Entry(ps).State = EntityState.Modified;
            db.SaveChanges();
            IEnumerable<ORDERS_ITEM> listaInsumos = db.ORDERS_ITEM.Include(x => x.ORDER).Include(x => x.SUPPLIER_INSUMO).Where(x => x.ID_ORDER == ID_ORDER && x.ACTIVE == 1).ToList();

            return PartialView("_orderITems", listaInsumos);
        }
        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER ORDER = db.ORDERS.Find(id);
            if (ORDER == null)
            {
                return HttpNotFound();
            }
            return View(ORDER);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDER ORDER = db.ORDERS.Find(id);
            db.ORDERS.Remove(ORDER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetSuppliesList(int id)
        {
            IEnumerable<ORDERS_ITEM> listaSupplies = db.ORDERS_ITEM.Where(x => x.ID_ORDER == id ).ToList();
            return PartialView("_orderItems", listaSupplies);

        }
        public ActionResult AddSupplies(int? ID_SUPPLIER_INSUMO,int? quantity,int? ID_ORDER,int? ID_SUPPLIE)
        {
            ORDERS_ITEM ord = new ORDERS_ITEM
            {
                ID_ORDER = ID_ORDER,
                ID_SUPPLIER_INSUMO = ID_SUPPLIER_INSUMO,
                QUANTITY = quantity,
                ACTIVE = 1
            };

            db.ORDERS_ITEM.Add(ord);
            db.SaveChanges();

            IEnumerable<ORDERS_ITEM> listaSupplies = db.ORDERS_ITEM.Include(m=>m.ORDER).Include(m=>m.SUPPLIER_INSUMO).Where(x => x.ID_ORDER == ID_ORDER && x.ACTIVE == 1);
            return PartialView("_orderItems", listaSupplies);

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
