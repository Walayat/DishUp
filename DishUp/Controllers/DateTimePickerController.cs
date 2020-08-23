using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DishUp.Controllers
{
    public class DateTimePickerController : BaseController
    {

        // GET: DateTimePicker
        public ActionResult Index()
        {
            dishupEntities db = new dishupEntities();
            CATERING c = db.CATERINGs.Where(x => x.ID_CATERING == 5).FirstOrDefault();
            return View(c);
        }
    }
}