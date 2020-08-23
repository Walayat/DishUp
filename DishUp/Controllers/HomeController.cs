using DishUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DishUp.Controllers
{
    public class HomeController : BaseController
    {
        private dishupEntities db= new dishupEntities();
        public ActionResult Index()
        {
           // List<PRODUCTO> listaProductos = db.MENU_PRODUCTO.Where(x => x.ID_MENU == 1).Select(x => x.PRODUCTO).ToList();
           // List<INSUMO> listaInsumo = db.MENU_INSUMO.Where(x => x.ID_MENU == 6).Select(x => x.INSUMO).ToList();


           // Dictionary<string, int?> listaInsumos = new Dictionary<string, int?>();
           
            
           // //TRAIGO PRODUCTOS DEL MENU 
           // for (int i = 0; i < 20; i++) { 
         
           //     //POR CADA PRODUCTO DEL MENU TRAIGO LOS INSUMOS CON SUS VALORES

           //     foreach (PRODUCTO prod in listaProductos)
           // {
           //     Dictionary<string, int?> listaIns = new Dictionary<string , int?>();
           //     listaIns = db.PRODUCTO_INSUMO.Where(x => x.ID_PRODUCTO == prod.ID_PRODUCTO).Select(x => new { x.INSUMO.NOMBRE, x.quantity }).ToDictionary(t=>t.NOMBRE,t=> t.quantity);

           //         foreach (KeyValuePair<string, int?> list in listaIns)
           //     {
           //         if (!listaInsumos.ContainsKey(list.Key))
           //         {
           //             listaInsumos.Add(list.Key, list.Value);
           //         }
           //         else
           //         {
           //             var valorInsumo = listaInsumos.Where(x=>x.Key==list.Key).Select(x=> x.Value).First();
           //             valorInsumo = valorInsumo + list.Value;
           //             listaInsumos[list.Key]= valorInsumo;
           //         }
           //     }

           // }
           ////TRAIGO LA LISTA DE INSUMOS QUE NECESITA EL MENU
               
           //         Dictionary<string, int?> listaInsu = new Dictionary<string, int?>();
           //         listaInsu = db.MENU_INSUMO.Where(x => x.ID_MENU == 6).Select(x => new { x.INSUMO.NOMBRE, x.QUANTITY }).ToDictionary(t => t.NOMBRE, t => t.QUANTITY);

           //         foreach (KeyValuePair<string, int?> list in listaInsu)
           //         {
           //             if (!listaInsumos.ContainsKey(list.Key))
           //             {
           //                 listaInsumos.Add(list.Key, list.Value);
           //             }
           //             else
           //             {
           //                 var valorInsumo = listaInsumos.Where(x => x.Key == list.Key).Select(x => x.Value).First();
           //                 valorInsumo = valorInsumo + list.Value;
           //                 listaInsumos[list.Key] = valorInsumo;
           //             }
           //         }
                                               

           // }
          
           // var l = listaInsumos;

           // var s = (from l_insumo in listaInsumos
           //         join ins in db.INSUMOes on l_insumo.Key equals ins.NOMBRE
           //         join med in db.MEDIDAs on ins.ID_MEDIDA equals med.ID_MEDIDA
           //         select new InsumosViewModel
           //         {
           //             id = ins.ID_INSUMO,
           //             nombreInsumo =    l_insumo.Key,
           //           nombre =   med.NOMBRE,
           //           quantity =   l_insumo.Value
           //         }).ToList();

           //var  xs = s;



            ViewBag.ID_PRODUCTO = new SelectList(db.PRODUCTOes, "ID_PRODUCTO", "NOMBRE");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}