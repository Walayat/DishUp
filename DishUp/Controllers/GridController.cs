using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DishUp.Controllers
{
    public class GridController : BaseController
    {
        // GET: Grid
        public ActionResult Index()
        {
            return View();
        }
    }
}