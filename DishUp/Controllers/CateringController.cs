using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DishUp;
using DishUp.Models;

namespace DishUp.Controllers
{
    public class CateringController : BaseController
    {
        private dishupEntities db = new dishupEntities();

        #region ABM
        // GET: CATERINGs
        public ActionResult Index()
        {
            var cATERINGs = db.CATERINGs.Include(c => c.CLIENT).Include(c => c.MENU).Where(x => x.deleted == 0);
            return View(cATERINGs.ToList());
        }

        // GET: CATERINGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATERING cATERING = db.CATERINGs.Find(id);
            if (cATERING == null)
            {
                return HttpNotFound();
            }
            return View(cATERING);
        }

        // GET: CATERINGs/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLIENT = new SelectList(db.CLIENTs, "ID_CLIENT", "NAME");
            ViewBag.ID_MENU = new SelectList(db.MENUs, "ID_MENU", "NOMBRE");
            return View();
        }

        // POST: CATERINGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CATERING,ARRIVAL_TIME,DEPARTURE_TIME,ID_CLIENT,ID_MENU,AMOUNT_PEOPLE,ADDRESS,EVENT_DATE,NOTES,HAS_PARKING")] CATERING cATERING)
        {
            if (ModelState.IsValid)
            {
                List<PRODUCTO> listaProductos = db.MENU_PRODUCTO.Where(x => x.ID_MENU == cATERING.ID_MENU).Select(x => x.PRODUCTO).ToList();

                List<INSUMO> listaInsumo = db.MENU_INSUMO.Where(x => x.ID_MENU == cATERING.ID_MENU && x.ACTIVE == 1).Select(x => x.INSUMO).ToList();

                List<CAT_INS_PACK> packagingCatering = new List<CAT_INS_PACK>();

                Dictionary<string, int?> listaInsumos = new Dictionary<string, int?>();

                var cantPersonas = cATERING.AMOUNT_PEOPLE;
                cATERING.deleted = 0;


                StringBuilder errorPackaging = new StringBuilder();

                db.CATERINGs.Add(cATERING);


                for (int i = 0; i < cantPersonas; i++)
                {
                    Dictionary<string, int?> listaInsu = new Dictionary<string, int?>();
                    listaInsu = db.MENU_INSUMO.Where(x => x.ID_MENU == cATERING.ID_MENU && x.ACTIVE == 1).Select(x => new { x.INSUMO.NOMBRE, x.QUANTITY }).ToDictionary(t => t.NOMBRE, t => t.QUANTITY);

                    foreach (KeyValuePair<string, int?> list in listaInsu)
                    {
                        if (!listaInsumos.ContainsKey(list.Key))
                        {
                            listaInsumos.Add(list.Key, list.Value);
                        }
                        else
                        {
                            var valorInsumo = listaInsumos.Where(x => x.Key == list.Key).Select(x => x.Value).First();
                            valorInsumo = valorInsumo + list.Value;
                            listaInsumos[list.Key] = valorInsumo;
                        }
                    }


                }


                //GENERO LISTA PARA INSERTAR LOS INSUMOS.

                var l = listaInsumos;
                var s = (from l_insumo in listaInsumos
                         join ins in db.INSUMOes on l_insumo.Key equals ins.NOMBRE
                         join med in db.MEDIDAs on ins.ID_MEDIDA equals med.ID_MEDIDA
                         select new InsumosViewModel
                         {
                             id = ins.ID_INSUMO,
                             nombreInsumo = l_insumo.Key,
                             nombre = med.NOMBRE,
                             quantity = l_insumo.Value
                         }).ToList();
                foreach (InsumosViewModel it in s)
                {


                    INSUMO_PACKAGING ip = db.INSUMO_PACKAGING.Where(x => x.ID_INSUMO == it.id && cantPersonas <= x.maxPerson && cantPersonas >= x.minPerson).OrderBy(x => x.QUANTITY).FirstOrDefault();
                    try
                    {
                        var cantPackaging = it.quantity / ip.QUANTITY;
                        if (cantPackaging == 0) { cantPackaging = 1; }
                        db.CAT_INS_PACK.Add(new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            QUANTITY = cantPackaging,
                            ID_INSUMO = it.id,
                            ID_PACKAGING = ip.ID_PACKAGING,
                            ACTIVE = 1


                        });
                    }
                    catch (Exception ex)
                    {
                        errorPackaging.AppendLine("Id Insumo:" + it.id + " ex: " + ex.InnerException);
                        db.INSUMO_PACKAGING_PENDING.Add(new INSUMO_PACKAGING_PENDING
                        {
                            ACTIVE = true,
                            ID_INSUMO = it.id,
                            QUANTITY = it.quantity,

                        });
                    }

                    if (it.id == 39)
                    {
                        double quant = Convert.ToDouble(it.quantity / 2);
                        db.CATERING_INSUMO.Add(new CATERING_INSUMO
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = Convert.ToInt16(it.id),
                            QUANTITY = quant,
                            ACTIVE = 1
                        });
                    }
                    else
                    {
                        db.CATERING_INSUMO.Add(new CATERING_INSUMO
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = Convert.ToInt16(it.id),
                            QUANTITY = it.quantity,
                            ACTIVE = 1

                        });
                    }

                }

                //AGREGO INSUMOS EXTRA POR MENU.

                //switch (cATERING.ID_MENU)
                //{
                //    case 1:
                //        break;
                //    case 2:
                //        CATERING_INSUMO CAT = new CATERING_INSUMO
                //        {
                //            ID_INSUMO = 66,
                //            QUANTITY = 500,
                //            ID_CATERING = cATERING.ID_CATERING
                //        };
                //        db.CATERING_INSUMO.Add(CAT);
                //        break;

                //}            

                //PREPARO PACKAGING.

                foreach (InsumosViewModel ins in s)
                {

                }
                switch (cATERING.ID_MENU)
                {
                    case 1:

                        break;
                    case 2:
                        double cateringBox = Convert.ToDouble(cantPersonas) / 10;
                        cateringBox = Math.Round(cateringBox);

                        //Extra Corn Chips.
                        CAT_INS_PACK cornChips = new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = 64,
                            QUANTITY = 1,
                            ID_PACKAGING = 8
                        };
                        //Hot Sauce.
                        CAT_INS_PACK hotSauce = new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = 23,
                            QUANTITY = 1,
                            ID_PACKAGING = 5
                        };
                        //Extra Guacamole
                        CAT_INS_PACK exTraGuacamole = new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = 66,
                            QUANTITY = 1,
                            ID_PACKAGING = 4
                        };
                        //CATERING BOX
                        CAT_INS_PACK catBox = new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = 66,
                            QUANTITY = Convert.ToInt16(cateringBox),
                            ID_PACKAGING = 8
                        };

                        db.CAT_INS_PACK.Add(cornChips);
                        db.CAT_INS_PACK.Add(hotSauce);
                        db.CAT_INS_PACK.Add(exTraGuacamole);
                        db.CAT_INS_PACK.Add(catBox);

                        break;
                    case 3:
                        double cornChipsCant = Convert.ToDouble(cantPersonas) / 15;
                        cornChipsCant = Math.Round(cornChipsCant);

                        //Extra Corn Chips.
                        CAT_INS_PACK cornChips2 = new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = 64,
                            QUANTITY = Convert.ToInt16(cornChipsCant),
                            ID_PACKAGING = 8
                        };

                        //BlackBowl 
                        CAT_INS_PACK blackBowl = new CAT_INS_PACK
                        {
                            ID_CATERING = cATERING.ID_CATERING,
                            ID_INSUMO = 67,
                            QUANTITY = cantPersonas,
                            ID_PACKAGING = 4
                        };

                        db.CAT_INS_PACK.Add(cornChips2);
                        db.CAT_INS_PACK.Add(blackBowl);

                        break;

                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;



                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLIENT = new SelectList(db.CLIENTs, "ID_CLIENT", "NAME", cATERING.ID_CLIENT);
            ViewBag.ID_MENU = new SelectList(db.MENUs, "ID_MENU", "NOMBRE", cATERING.ID_MENU);
            return View(cATERING);
        }
        public ActionResult PackagingPending(int id)
        {
            return PartialView("_listaPendingPackaging");
        }
        // GET: CATERINGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CateringViewModel catering = db.CATERINGs.Select(x => new CateringViewModel
            {
                EVENT_DATE = x.EVENT_DATE,
                EVENT_TIME = x.EVENT_TIME,
                ADDRESS = x.ADDRESS,
                AMOUNT_PEOPLE = x.AMOUNT_PEOPLE,
                ID_CATERING = x.ID_CATERING,
                ID_MENU = x.ID_MENU,
                ID_CLIENT = x.ID_CLIENT,
                NOTES = x.NOTES,
                ARRIVAL_TIME = x.ARRIVAL_TIME,
                DEPARTURE_TIME = x.DEPARTURE_TIME
            }).Where(x => x.ID_CATERING == id).FirstOrDefault();

            // CATERING cATERING = db.CATERINGs.Find(id);
            if (catering == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLIENT = new SelectList(db.CLIENTs, "ID_CLIENT", "NAME", catering.ID_CLIENT);
            ViewBag.ID_MENU = new SelectList(db.MENUs, "ID_MENU", "NOMBRE", catering.ID_MENU);
            ViewBag.MENU = new SelectList(db.INSUMOes, "ID_MENU", "NOMBRE");
            ViewBag.PRODUCTO= new SelectList(db.PRODUCTOes, "ID_PRODUCTO", "NOMBRE");
            ViewBag.INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            ViewBag.ID_MENU = new SelectList(db.MENUs, "ID_MENU", "NOMBRE", catering.ID_MENU);
            catering.HAS_PARKING = false;
            return View(catering);
        }

        // POST: CATERINGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CATERING,ID_CLIENT,ID_MENU,AMOUNT_PEOPLE,ADDRESS,EVENT_DATE,EVENT_TIME,NOTES,HAS_PARKING,DELETED")] CATERING cATERING)
        {
            if (ModelState.IsValid)
            {
                cATERING.deleted = 0;
                db.Entry(cATERING).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLIENT = new SelectList(db.CLIENTs, "ID_CLIENT", "NAME", cATERING.ID_CLIENT);
            ViewBag.ID_MENU = new SelectList(db.MENUs, "ID_MENU", "NOMBRE", cATERING.ID_MENU);
            return View(cATERING);
        }

        // GET: CATERINGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATERING cATERING = db.CATERINGs.Find(id);
            if (cATERING == null)
            {
                return HttpNotFound();
            }
            return View(cATERING);
        }

        // POST: CATERINGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATERING cATERING = db.CATERINGs.Find(id);
            cATERING.deleted = 1;
            db.Entry(cATERING).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void SendEmail(string email, int id)
        {
            MailManager mm = new MailManager();

            CreatePDF(id);

            CATERING catering = db.CATERINGs.Where(x => x.ID_CATERING == id).FirstOrDefault();
            var body = "Please find attached the catering information.";

            SmtpClient smtpCliente = new SmtpClient();
            smtpCliente.Host = "relay-hosting.secureserver.net ";
            smtpCliente.Port = 25;
            smtpCliente.EnableSsl = false;
            smtpCliente.UseDefaultCredentials = true;
            smtpCliente.Credentials = new NetworkCredential { UserName = "enquiries@DishUp.com.au", Password = "Enqu1r1e5atp4blo" };
            var mailMEssage = new MailMessage();
            mailMEssage.From = new MailAddress("enquiries@DishUp.com.au", "Catering Order");
            mailMEssage.Subject = "NEW ORDER. SCHEDULED DATE: " + catering.EVENT_DATE.Value.ToShortDateString() + " from " + catering.CLIENT.NAME + " " + catering.CLIENT.SURNAME;
            mailMEssage.Body = body;
            mailMEssage.To.Add(email);
            try
            {
                mailMEssage.Attachments.Add(new Attachment(Server.MapPath("~/Uploads/" + catering.CLIENT.NAME + "_" + catering.CLIENT.SURNAME + ".pdf")));

            }
            catch (Exception)
            {

                throw;
            }
            mailMEssage.IsBodyHtml = true;
            smtpCliente.Send(mailMEssage);

        }

        public  ActionResult GetCateringCostView(int ID_CATERING)
        {
            return PartialView("_itemsList", GetCateringCost(ID_CATERING));
        }
        public TempOrderViewModel GetCateringCost(int ID_CATERING)
        {
            double? costoMenu = 0;
            double? costoProducto = 0;
            double? costoSupplie = 0;
            double? costoTotal = 0;

            /*PACKAGES*/
            var listaMenu = db.CATERING_MENU.Include(x => x.MENU).Where(x => x.ID_CATERING == ID_CATERING).ToList();

            foreach (CATERING_MENU menu in listaMenu)
            {
                costoMenu += (menu.QUANTITY * Convert.ToDouble(menu.MENU.COST));
            }
            /*PRODUCTS*/
            var listaProductos = db.CATERING_PRODUCTO.Include(x => x.PRODUCTO).Where(x => x.ID_CATERING == ID_CATERING).ToList();
            foreach (CATERING_PRODUCTO prod in listaProductos)
            {
                costoProducto +=  (prod.QUANTITY * Convert.ToDouble(prod.PRODUCTO.WHOLESALE_PRICE));
            }

            /*SUPPLIES*/
            var listaInsumos = db.CATERING_INSUMO.Include(x => x.INSUMO).Where(x => x.ID_CATERING == ID_CATERING).ToList();

            foreach (CATERING_INSUMO ins in listaInsumos)
            {
                costoSupplie += (ins.QUANTITY * Convert.ToDouble(ins.INSUMO.WHOLESALE_PRICE));
            }
            costoTotal = costoMenu + costoProducto + costoSupplie;


            return new TempOrderViewModel  { listaInsumos = listaInsumos, listaMenu = listaMenu, listaProductos = listaProductos,totalPackageCost = Convert.ToDouble(costoTotal),costoMenu = costoMenu,costoProducto=costoProducto,costoSupplie=costoSupplie };
        }
        public ActionResult AddCateringSupplie(int INSUMO, string QUANT_SUPPLIE,int ID_CATERING,int tipo) {
            CATERING_INSUMO cat_ins = new CATERING_INSUMO
            {
                ID_CATERING = ID_CATERING,
                ID_INSUMO = INSUMO,
                QUANTITY = Convert.ToDouble(QUANT_SUPPLIE)
            };

            db.CATERING_INSUMO.Add(cat_ins);
            db.SaveChanges();

            TempOrderViewModel tovm = GetCateringCost(ID_CATERING);
            return PartialView("_itemsList",tovm );

        }
        public ActionResult AddCateringProducto(int PRODUCTO, string QUANT_PRODUCTO, int ID_CATERING) {

            CATERING_PRODUCTO cat_prod = new CATERING_PRODUCTO
            {
                ID_CATERING = ID_CATERING,
                ID_PRODUCTO = PRODUCTO,
                QUANTITY = Convert.ToDouble(QUANT_PRODUCTO)
            };

            db.CATERING_PRODUCTO.Add(cat_prod);
            db.SaveChanges();

            TempOrderViewModel tovm = GetCateringCost(ID_CATERING);
            return PartialView("_itemsList", tovm);
            

        }
        public ActionResult AddCateringMenu(int ID_MENU, string QUANT_MENU, int ID_CATERING) {
            CATERING_MENU cat_menu = new CATERING_MENU
            {
                ID_CATERING = ID_CATERING,
                ID_MENU = ID_MENU,
                QUANTITY = Convert.ToDouble(QUANT_MENU)
            };

            db.CATERING_MENU.Add(cat_menu);
            db.SaveChanges();
            TempOrderViewModel tovm = GetCateringCost(ID_CATERING);
            return PartialView("_itemsList", tovm);
        }





        [HttpGet]
        public ActionResult GetInsumos(int? id)
        {
            IEnumerable<CATERING_INSUMO> insumoList = db.CATERING_INSUMO.Where(x=> x.ID_CATERING== id && x.ACTIVE == 1 ).ToList();
            return PartialView("_listaInsumos", insumoList);
}

        public ActionResult GetPackaging(int? id)
        {
            IEnumerable<CAT_INS_PACK> packList = db.CAT_INS_PACK.Where(x => x.ID_CATERING == id && x.ACTIVE==1).ToList();
            return PartialView("_listaPackaging", packList);
        }

        public ActionResult NewSupplie(int id)
        {
            CATERING_INSUMO pr = new CATERING_INSUMO();
            pr.ID_CATERING = id;
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            return PartialView("_editSupplie", new CATERING_INSUMO { ID_CATERING = id });
        }
        public ActionResult NewPackaging(int id)
        {
            CAT_INS_PACK pr = new CAT_INS_PACK();
            pr.ID_CATERING = id;
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME");
            return PartialView("_editPackaging", new CAT_INS_PACK { ID_CATERING = id });
        }
        [HttpGet]
        public ActionResult EditSupplie(int id)
        {
            CATERING_INSUMO C = db.CATERING_INSUMO.Where(x => x.ID_CATERING_INSUMO == id).FirstOrDefault();
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes.Where(X=>X.ACTIVE==true), "ID_INSUMO", "NOMBRE",C.ID_INSUMO);

            return PartialView("_editSupplie",C);
        }
        [HttpPost]
        public ActionResult EditSupplieP(int ID_CATERING_INSUMO,int QUANTITY, int ID_INSUMO, int ID_CATERING)
        {
            CATERING_INSUMO cat = db.CATERING_INSUMO.Where(x => x.ID_CATERING_INSUMO == ID_CATERING_INSUMO).FirstOrDefault();

            //if (cat.ID_INSUMO == 0)
            //{
            //    ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");

            //    return PartialView("_editSupplie", new CATERING_INSUMO { ID_CATERING_INSUMO = ID_CATERING_INSUMO });

            //}
            CATERING_INSUMO cateringInsumo = db.CATERING_INSUMO.Include(x => x.INSUMO).Where(x => x.ID_CATERING_INSUMO == ID_CATERING_INSUMO    && x.ACTIVE == 1).FirstOrDefault();

            if (cateringInsumo != null)
            {
                cateringInsumo.QUANTITY = QUANTITY;
                db.Entry<CATERING_INSUMO>(cateringInsumo).State = EntityState.Modified;

            }
            else
            {
                CATERING_INSUMO PR = new CATERING_INSUMO
                {
                    ID_CATERING = ID_CATERING,
                    QUANTITY = QUANTITY,
                    ID_INSUMO = ID_INSUMO,
                    ACTIVE = 1

                };
                db.CATERING_INSUMO.Add(PR);
            }
            db.SaveChanges();
            IEnumerable<CATERING_INSUMO> listaInsumos = db.CATERING_INSUMO.Include(x => x.INSUMO).Where(x => x.ID_CATERING == ID_CATERING && x.ACTIVE == 1).ToList();
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");

            return PartialView("_listaInsumos", listaInsumos);
        }



        [HttpGet]
        public ActionResult EditPackaging(int id)
        {
            CAT_INS_PACK C = db.CAT_INS_PACK.Where(x => x.ID_CAT_INS_PACK == id).FirstOrDefault();
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes.Where(X => X.ACTIVE == true), "ID_INSUMO", "NOMBRE", C.ID_INSUMO);
            //ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs.Where(X => X.ACTIVE == 1), "ID_PACKAGING", "NOMBRE", C.ID_PACKAGING);
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME", C.ID_PACKAGING);

            return PartialView("_editPackaging", C);
        }
        [HttpPost]
        public ActionResult EditPackagingP(int ID_CAT_INS_PACK, int QUANTITY, int ID_INSUMO, int ID_CATERING,int ID_PACKAGING)
        {
            CAT_INS_PACK cat = db.CAT_INS_PACK.Where(x => x.ID_CAT_INS_PACK == ID_CAT_INS_PACK).FirstOrDefault();

            //if (cat.ID_INSUMO == 0)
            //{
            //    ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");

            //    return PartialView("_editSupplie", new CATERING_INSUMO { ID_CATERING_INSUMO = ID_CATERING_INSUMO });

            //}
            CAT_INS_PACK cateringInsumo = db.CAT_INS_PACK.Include(x => x.INSUMO).Include(x=>x.CATERING).Include(x=>x.PACKAGING).Where(x => x.ID_CAT_INS_PACK == ID_CAT_INS_PACK && x.ACTIVE == 1).FirstOrDefault();

            if (cateringInsumo != null)
            {
                cateringInsumo.QUANTITY = QUANTITY;
                cateringInsumo.ID_INSUMO = ID_INSUMO;
                cateringInsumo.ID_PACKAGING = ID_PACKAGING;
                db.Entry<CAT_INS_PACK>(cateringInsumo).State = EntityState.Modified;

            }
            else
            {
                CAT_INS_PACK PR = new CAT_INS_PACK
                {
                    ID_CATERING = ID_CATERING,
                    QUANTITY = QUANTITY,
                    ID_INSUMO = ID_INSUMO,
                    ID_PACKAGING = ID_PACKAGING,
                   ACTIVE = 1

                };
                db.CAT_INS_PACK.Add(PR);
            }
            db.SaveChanges();
            IEnumerable<CAT_INS_PACK> listaInsumos = db.CAT_INS_PACK.Include(x => x.INSUMO).Include(x=>x.CATERING).Where(x => x.ID_CATERING == ID_CATERING && x.ACTIVE == 1).ToList();
            ViewBag.ID_INSUMO = new SelectList(db.INSUMOes, "ID_INSUMO", "NOMBRE");
            ViewBag.ID_PACKAGING = new SelectList(db.PACKAGINGs, "ID_PACKAGING", "NAME");

            return PartialView("_listaPackaging", listaInsumos);
        }

        public ActionResult DeleteCateringSupplie(int id, int ID_CATERING)
        {
            CATERING_INSUMO m = db.CATERING_INSUMO.Find(id);
            m.ACTIVE = 0;
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();

            IEnumerable<CATERING_INSUMO> listaProductos = db.CATERING_INSUMO.Include(x => x.INSUMO).Where(x => x.ID_CATERING == ID_CATERING && x.ACTIVE == 1).ToList();

            return PartialView("_listaInsumos", listaProductos);
        }
        public ActionResult DeleteCateringPackaging(int id, int ID_CATERING)
        {
            CAT_INS_PACK m = db.CAT_INS_PACK.Find(id);
            m.ACTIVE = 0;
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();

            IEnumerable<CAT_INS_PACK> listaProductos = db.CAT_INS_PACK.Include(x => x.INSUMO).Where(x => x.ID_CATERING == ID_CATERING && x.ACTIVE == 1).ToList();

            return PartialView("_listaPackaging", listaProductos);
        }



        #region JSONLIST


        [HttpGet]
        public JsonResult GetMenu(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "MENU");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetClient(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "CLIENT");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PDF
        public void CreatePDF(int ID)
        {
            //Bring the Catering Object.

            CATERING cat = db.CATERINGs.Find(ID);

            var filePath = Server.MapPath("~/Uploads/") + cat.CLIENT.NAME + "_" + cat.CLIENT.SURNAME + ".pdf";

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);

            }


            // Create a Document object
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            // Create a new PdfWrite object, writing the output to a MemoryStream
          
            var writer = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/Uploads/") + cat.CLIENT.NAME +"_"+cat.CLIENT.SURNAME + ".pdf",FileMode.Create));
            // Open the Document for writing;
            document.Open();

            // First, create our fonts... (For more on working w/fonts in iTextSharp, see: http://www.mikesdotnetting.com/Article/81/iTextSharp-Working-with-Fonts
            var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
            var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

            // Add the "Northwind Traders Receipt" title
            document.Add(new iTextSharp.text.Paragraph("Pablo's Kitchen Catering Order", titleFont));

            // Now add the "Thank you for shopping at Northwind Traders. Your order details are below." message
            document.Add(new Paragraph("Read the information carefully and make sure to double check the order.", bodyFont));
            document.Add(Chunk.NEWLINE);

            // Add the "Order Information" subtitle
            document.Add(new Paragraph("Order Information", subTitleFont));

            // Create the Order Information table - see http://www.mikesdotnetting.com/Article/86/iTextSharp-Introducing-Tables for more info
            var orderInfoTable = new PdfPTable(2);
            orderInfoTable.HorizontalAlignment = 0;
            orderInfoTable.SpacingBefore = 20;
            orderInfoTable.SpacingAfter = 20;
            orderInfoTable.DefaultCell.Border = 0;

            orderInfoTable.SetWidths(new int[] { 5, 9});
            orderInfoTable.AddCell(new Phrase("Client Name:", boldTableFont));
            orderInfoTable.AddCell( cat.CLIENT.NAME + " " + cat.CLIENT.SURNAME);
            orderInfoTable.AddCell(new Phrase("Contact Number:", boldTableFont));
            orderInfoTable.AddCell(cat.CLIENT.MOBILE_NUMBER);
            orderInfoTable.AddCell(new Phrase("Address:", boldTableFont));
            orderInfoTable.AddCell(cat.ADDRESS);
            orderInfoTable.AddCell(new Phrase("Delivery Date & Time:", boldTableFont));
            orderInfoTable.AddCell(cat.EVENT_DATE.Value.ToShortDateString() + " " + cat.EVENT_TIME );
            orderInfoTable.AddCell(new Phrase("Departure Time:", boldTableFont));
            orderInfoTable.AddCell(cat.DEPARTURE_TIME);
            orderInfoTable.AddCell(new Phrase("Arrival Time:", boldTableFont));
            orderInfoTable.AddCell(cat.ARRIVAL_TIME);

            document.Add(orderInfoTable);



            // Add the "Items In Your Order" subtitle
            document.Add(new Paragraph("Order Description", subTitleFont));

            // Create the Order Details table
            var orderDetailsTable = new PdfPTable(3);
            orderDetailsTable.HorizontalAlignment = 0;
            orderDetailsTable.SpacingBefore = 10;
            orderDetailsTable.SpacingAfter = 10;
            orderDetailsTable.DefaultCell.Border = 0;

            orderDetailsTable.AddCell(new Phrase("Supplie:", boldTableFont));
            orderDetailsTable.AddCell(new Phrase("Quantity:", boldTableFont));
            orderDetailsTable.AddCell(new Phrase("Packaging:", boldTableFont));

            List<CAT_INS_PACK> suppliesList = db.CAT_INS_PACK.Where(x => x.ID_CATERING == ID && x.ACTIVE == 1).ToList();
            StringBuilder strBuilder = new StringBuilder();
            foreach (CAT_INS_PACK ci in suppliesList)
            {

                orderDetailsTable.AddCell(new Phrase(ci.INSUMO.NOMBRE));
               
                if (ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value >= 1000)
                {
                    switch (ci.INSUMO.MEDIDA.NOMBRE)
                    {
                        case "Gramos":
                            var x = ci.CATERING.CATERING_INSUMO.Where(o=>o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o=>o.QUANTITY).FirstOrDefault().Value;
                             x = x / 1000;
                            orderDetailsTable.AddCell(x.ToString() + " Kilo/s");

                            break;
                        case "Ml":
                            var y = ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value;
                            y = y / 1000;
                            orderDetailsTable.AddCell(y.ToString() + " Litre/s");

                            break;
                        case "Unidad":
                            orderDetailsTable.AddCell(ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value + " Units");

                            
                            break;
                        default:
                            orderDetailsTable.AddCell(ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value.ToString());

                            break;
                    }
                }
                else
                {
                    switch (ci.INSUMO.MEDIDA.NOMBRE)
                    {
                        case "Gramos":
                            var x = ci.CATERING.CATERING_INSUMO.Where(o=>o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o=>o.QUANTITY).FirstOrDefault().Value;
                            orderDetailsTable.AddCell(x.ToString() + " Gramos.");

                            break;
                        case "Ml":
                            var y = ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value;
                            orderDetailsTable.AddCell(y.ToString() + " Ml.");

                            break;
                        case "Unidad":
                            orderDetailsTable.AddCell(ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value + " Units");


                            break;
                        default:
                            orderDetailsTable.AddCell(ci.CATERING.CATERING_INSUMO.Where(o => o.ID_INSUMO == ci.ID_INSUMO && o.ID_CATERING == ID).Select(o => o.QUANTITY).FirstOrDefault().Value + " " + ci.INSUMO.MEDIDA.NOMBRE);

                            break;
                    }
                }
                orderDetailsTable.AddCell(ci.QUANTITY.ToString() + " " + ci.PACKAGING.NAME);
                
            }







            document.Add(orderDetailsTable);


            // Add ending message
            var endingMessage = new Paragraph("Please make sure to double check the order. If you have any doubt about it, please contact your Supervisor.", endingMessageFont);
            endingMessage.Alignment = Element.ALIGN_CENTER;

            document.Add(endingMessage);

            // Finally, add an image in the upper right corner
            var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/Logo-DishUp.png"));
            logo.ScaleAbsoluteWidth(120F);
            logo.ScaleAbsoluteHeight(100F);
            logo.SetAbsolutePosition(480, 730);
            document.Add(logo);

            document.Close();
         
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Receipt-{0}.pdf", cat.CLIENT.NAME));
        }
        #endregion

    }
}
