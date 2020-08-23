using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Controllers
{
    public class Select2Repository : BaseController
    {

        private dishupEntities db = new dishupEntities();

        private IQueryable<Select2Result> GetMenu()
        {
            string cacheKey = "Select2Menu";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var menues = db.MENUs.Where(x => x.ACTIVE == true).ToList();
            foreach (MENU p in menues)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_MENU.ToString(),
                    text = p.NOMBRE
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }
        private IQueryable<Select2Result> GetClient()
        {
            string cacheKey = "Select2Client";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var clients = db.CLIENTs.Where(x=>x.ACTIVE == true).ToList();
            foreach (CLIENT p in clients)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_CLIENT.ToString(),
                    text = p.NAME.ToString()
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }
        private IQueryable<Select2Result> GetSupplie()
        {
            string cacheKey = "Select2Supplie";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var supplies = db.USER_SUPPLIER_INSUMO.Where(x=> x.ACTIVE == true).ToList();
            foreach (USER_SUPPLIER_INSUMO p in supplies)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_USER_SUPPLIER_INSUMO.ToString(),
                    text = p.NAME
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }
        private IQueryable<Select2Result> GetSupplier()
        {
            string cacheKey = "Select2Supplier";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var supplier = db.SUPPLIERs.Where(x => x.ACTIVE == true).ToList();
            foreach (SUPPLIER p in supplier)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_SUPPLIER.ToString(),
                    text = p.NAME
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }
        private IQueryable<Select2Result> GetMySupplier()
        {
            string cacheKey = "Select2MySupplier";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }
            var idUser = User.Identity.GetUserId();
            var optionList = new List<Select2Result>();
            var supplier = db.USER_SUPPLIER.Where(x => x.ACTIVE == true && x.ID_USER  == idUser).ToList();
            foreach (USER_SUPPLIER p in supplier)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_USER_SUPPLIER.ToString(),
                    text = p.NAME
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }

        private IQueryable<Select2Result> GetSupplierSupplie( int? ID = null)
        {
            string cacheKey = "Select2SupplierSupplie";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var supplier = db.SUPPLIER_INSUMO.Where(x => x.ACTIVE == true && x.ID_SUPPLIER == ID).ToList();
            foreach (SUPPLIER_INSUMO p in supplier)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_SUPPLIER_INSUMO.ToString(),
                    text = p.INSUMO.NOMBRE
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }

        private IQueryable<Select2Result> GetMySupplierSupplie(int? ID = null)
        {
            string cacheKey = "Select2MySupplierSupplie";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var supplier = db.SUPPLIER_INSUMO.Where(x => x.ACTIVE == true && x.ID_SUPPLIER == ID).ToList();
            foreach (SUPPLIER_INSUMO p in supplier)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_SUPPLIER_INSUMO.ToString(),
                    text = p.INSUMO.NOMBRE
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }

        private IQueryable<Select2Result> GetSupplierSupplieRelated(int? ID = null)
        {
            string cacheKey = "Select2SupplierSupplieRelated";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var supplier = db.SUPPLIER_SUPPLIE.Where(x => x.ACTIVE == true && x.ID_INSUMO == ID).ToList();
            foreach (SUPPLIER_SUPPLIE p in supplier)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_SUPPLIER.ToString(),
                    text = p.SUPPLIER.NAME
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }

        private IQueryable<Select2Result> GetProduct()
        {
            string cacheKey = "Select2Product";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var PROD = db.PRODUCTOes.Where(x=> x.ACTIVE == true).ToList();
            foreach (PRODUCTO p in PROD)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_PRODUCTO.ToString(),
                    text = p.NOMBRE
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        
        }
        private IQueryable<Select2Result> GetRecipe()
        {
            string cacheKey = "Select2REcipe";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var PROD = db.RECIPEs.Where(x => x.ACTIVE == true ).ToList(); //AGREGAR USUARIO
            foreach (RECIPE p in PROD)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_RECIPE.ToString(),
                    text = p.NAME
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;

        }
        private IQueryable<Select2Result> GetPackaging()
        {
            string cacheKey = "Select2Packaging";
            //check cache
            if (HttpContext.Cache[cacheKey] != null)
            {
                return (IQueryable<Select2Result>)HttpContext.Cache[cacheKey];
            }

            var optionList = new List<Select2Result>();
            var PROD = db.PACKAGINGs.Where(x => x.ACTIVE == 1).ToList();
            foreach (PACKAGING p in PROD)
            {
                optionList.Add(new Select2Result
                {
                    id = p.ID_PACKAGING.ToString(),
                    text = p.NAME
                });
            }
            var result = optionList.AsQueryable();

            //cache results
            HttpContext.Cache[cacheKey] = result;
            return result;
        }
        private List<Select2Result> GetPagedListOptions(string searchTerm, int pageSize, int pageNumber, out int totalSearchRecords, string dbtable, int? id = null)
        {
            if (id != null)
            {
                var allSearchedResults = GetAllSearchResults(searchTerm, dbtable,id);
                totalSearchRecords = allSearchedResults.Count;
                return allSearchedResults.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            }
            else
            {

                var allSearchedResults = GetAllSearchResults(searchTerm, dbtable,id);
                totalSearchRecords = allSearchedResults.Count;
                return allSearchedResults.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            }
        }
        public Select2PagedResult GetSelect2PagedResult(string searchTerm, int pageSize, int pageNumber, string dbtable,int? id = null )
        {
            var select2pagedResult = new Select2PagedResult();

            var totalResults = 0;

            select2pagedResult.Results = GetPagedListOptions(searchTerm,

            pageSize, pageNumber, out totalResults, dbtable,id);

            select2pagedResult.Total = totalResults;

            return select2pagedResult;
        }
        private List<Select2Result> GetAllSearchResults(string searchTerm, string dbtable,int? id = null)
        {
            var resultList = new List<Select2Result>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (dbtable)
                {
                    case "MENU":
                        resultList = GetMenu().Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;

                    case "CLIENT":
                        resultList = GetClient().Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "SUPPLIE":
                        resultList = GetSupplie().Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "MYSUPPLIER":
                        resultList = GetMySupplier().Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "SUPPLIER_SUPPLIE":
                        resultList = GetSupplierSupplie(id).Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "MYSUPPLIERSUPPLIE":
                        resultList = GetMySupplierSupplie(id).Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "SUPPLIER_SUPPLIE_RELATED":
                        resultList = GetSupplierSupplieRelated(id).Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "PRODUCT":
                        resultList = GetProduct().Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                    case "RECIPE":
                        resultList = GetRecipe().Where(n => n.text.ToLower().Contains(searchTerm.ToLower())).ToList();
                        break;
                }
            }
            else
            {
                switch (dbtable)
                {
                    case "MENU":
                        resultList = GetMenu().ToList();
                        break;
                    case "CLIENT":
                        resultList = GetClient().ToList();
                        break;
                    case "SUPPLIE":
                        resultList = GetSupplie().ToList();
                        break;
                    case "MYSUPPLIERSUPPLIE":
                        resultList = GetMySupplierSupplie().ToList();
                        break;
                    case "MYSUPPLIER":
                        resultList = GetMySupplier().ToList();
                        break;
                    case "SUPPLIER":
                        resultList = GetSupplier().ToList();
                        break;
                    case "SUPPLIER_SUPPLIE":
                        resultList = GetSupplierSupplie(id).ToList();
                        break;
                    case "SUPPLIER_SUPPLIE_RELATED":
                        resultList = GetSupplierSupplieRelated(id).ToList();
                        break;
                    case "PRODUCT":
                        resultList = GetProduct().ToList();
                        break;

                    case "RECIPE":
                        resultList = GetRecipe().ToList();
                        break;
                    case "PACKAGING":
                        resultList = GetPackaging().ToList();
                        break;
                }
            }

            return resultList;
        }


    }
}