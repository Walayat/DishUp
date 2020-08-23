using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DishUp.Controllers
{
    public class JsonController : BaseController
    {
        // GET: Json
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetSupplies(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "SUPPLIE");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSupplierSupplie(string searchTerm, int pageSize, int pageNumber, int ID)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "SUPPLIER_SUPPLIE",ID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetPackaging(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "PACKAGING");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSupplier(string searchTerm, int pageSize, int pageNumber)
        {
            var select2Repository = new Select2Repository();

            var result = select2Repository.GetSelect2PagedResult(searchTerm, pageSize, pageNumber, "SUPPLIER");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}