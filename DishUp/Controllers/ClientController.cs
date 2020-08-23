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

namespace DishUp.Controllers
{
    public class ClientController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        // GET: Client
        public ActionResult Index()
        {
            return View(db.CLIENTs.Where(x=>x.ACTIVE== true).ToList());
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENTs.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cLIENT);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CLIENT,NAME,SURNAME,HOME_NUMBER,MOBILE_NUMBER,EMAIL")] ClientViewModel cLIENT)
        {
            if (ModelState.IsValid)
            {
                CLIENT client = new CLIENT
                {
                    NAME = cLIENT.NAME,
                    SURNAME = cLIENT.SURNAME,
                    HOME_NUMBER = cLIENT.HOME_NUMBER,
                    MOBILE_NUMBER = cLIENT.MOBILE_NUMBER,
                    EMAIL = cLIENT.EMAIL
                };
        
                client.ACTIVE = true;
                client.DELETED = false;
                db.CLIENTs.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cLIENT);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENTs.Find(id);
            ClientViewModel cliente = new ClientViewModel
            {
                NAME = cLIENT.NAME,
                SURNAME = cLIENT.SURNAME,
                HOME_NUMBER = cLIENT.HOME_NUMBER,
                MOBILE_NUMBER = cLIENT.MOBILE_NUMBER,
                EMAIL = cLIENT.EMAIL,
                ID_CLIENT = cLIENT.ID_CLIENT
            };
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CLIENT,NAME,SURNAME,HOME_NUMBER,MOBILE_NUMBER,EMAIL")] ClientViewModel cLIENT)
        {
            if (ModelState.IsValid)
            {
                CLIENT cliente = new CLIENT
                {
                    NAME = cLIENT.NAME,
                    SURNAME = cLIENT.SURNAME,
                    HOME_NUMBER = cLIENT.HOME_NUMBER,
                    MOBILE_NUMBER = cLIENT.MOBILE_NUMBER,
                    EMAIL = cLIENT.EMAIL,
                    ID_CLIENT = cLIENT.ID_CLIENT,
                    
                };

                cliente.ACTIVE = true;
                cliente.DELETED = false;
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLIENT);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENTs.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cLIENT);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLIENT cLIENT = db.CLIENTs.Find(id);
            cLIENT.ACTIVE = false;
            db.Entry(cLIENT).State = EntityState.Modified;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetHistory( int id)
        {
            List<ClientHistoryViewModel> clientHistory = new List<ClientHistoryViewModel>();

           List<CATERING> clientOrders = db.CATERINGs.Where(x => x.ID_CLIENT == id).ToList();
            foreach(var items in clientOrders)
            {
                clientHistory.Add(new ClientHistoryViewModel {
                    ID_CATERING = items.ID_CATERING,
                    clientName = items.CLIENT.NAME + " " + items.CLIENT.SURNAME,
                    eventDate = items.EVENT_DATE +   " "  + items.EVENT_TIME
                });
            }
            return PartialView("_history", clientHistory);

        }

        public ActionResult GetOrderInfo(int id)
        {

            return PartialView("_clientOrderInfo", id);
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
