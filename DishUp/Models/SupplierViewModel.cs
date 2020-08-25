using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class SupplierViewModel
    {
        public int ID_SUPPLIER { get; set; }
        public int ID_USER_SUPPLIER { get; set; }
        [Required(ErrorMessage = "Please enter a valid Name")]
        public string NAME { get; set; }
        [Required(ErrorMessage = "Please enter a valid Surname")]
        public string SURNAME { get; set; }

        public string MOBILE_NUMBER { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string ABN_NUMBER { get; set; }
        //[Required(ErrorMessage = "Please enter an ACN Number")]
        public string ACN_NUMBER { get; set; }
        [Required(ErrorMessage = "Please enter the email Address")]
        public string EMAIL { get; set; }
        public string OPEN_HOURS { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
        public string DELIVERY_DAYS { get; set; }
        public int IS_FROM_USER { get; set; }

        public static implicit operator SupplierViewModel(SUPPLIER sUPPLIER)
        {
            return new SupplierViewModel
            {

                ID_SUPPLIER = sUPPLIER.ID_SUPPLIER,
                NAME = sUPPLIER.NAME,
                SURNAME = sUPPLIER.SURNAME,
                MOBILE_NUMBER = sUPPLIER.MOBILE_NUMBER,
                PHONE_NUMBER = sUPPLIER.PHONE_NUMBER,
                ABN_NUMBER = sUPPLIER.ABN_NUMBER,
                ACN_NUMBER = sUPPLIER.ACN_NUMBER,
                EMAIL = sUPPLIER.EMAIL,
                OPEN_HOURS = sUPPLIER.OPEN_HOURS,
                DELIVERY_DAYS = sUPPLIER.DELIVERY_DAYS,
                ACTIVE = true
            };

        }
        public static implicit operator SUPPLIER(SupplierViewModel sUPPLIER)
        {
            return new SUPPLIER
            {

                ID_SUPPLIER = sUPPLIER.ID_SUPPLIER,
                NAME = sUPPLIER.NAME,
                SURNAME = sUPPLIER.SURNAME,
                MOBILE_NUMBER = sUPPLIER.MOBILE_NUMBER,
                PHONE_NUMBER = sUPPLIER.PHONE_NUMBER,
                ABN_NUMBER = sUPPLIER.ABN_NUMBER,
                ACN_NUMBER = sUPPLIER.ACN_NUMBER,
                EMAIL = sUPPLIER.EMAIL,
                OPEN_HOURS = sUPPLIER.OPEN_HOURS,
                DELIVERY_DAYS = sUPPLIER.DELIVERY_DAYS,
                ACTIVE = true
            };

        }

        public static implicit operator SupplierViewModel(USER_SUPPLIER sUPPLIER)
        {
            return new SupplierViewModel
            {

                ID_USER_SUPPLIER = sUPPLIER.ID_USER_SUPPLIER,
                ID_SUPPLIER= Convert.ToInt32(sUPPLIER.ID_SUPPLIER),
                NAME = sUPPLIER.NAME,
                SURNAME = sUPPLIER.SURNAME,
                MOBILE_NUMBER = sUPPLIER.MOBILE_NUMBER,
                PHONE_NUMBER = sUPPLIER.PHONE_NUMBER,
                ABN_NUMBER = sUPPLIER.ABN_NUMBER,
                ACN_NUMBER = sUPPLIER.ACN_NUMBER,
                EMAIL = sUPPLIER.EMAIL,
                OPEN_HOURS = sUPPLIER.OPEN_HOURS,
                ACTIVE = true
            };

        }
       

    }


    public class SupplierInsumoViewModel
    {

        public int ID_SUPPLIER_INSUMO { get; set; }
        public Nullable<int> ID_SUPPLIER { get; set; }
        public Nullable<int> ID_INSUMO { get; set; }
        public bool SELECTED { get; set; }
        public bool ACTIVE { get; set; }
        public string CODE { get; set; }
        [Required(ErrorMessage = "You must enter name or description for the product")]

        public string NAME { get; set; }
        [Required(ErrorMessage = "You must enter a valid Price")]

        public decimal? PRICE { get; set; }
        
        public bool? IS_PACKAGE { get; set; }
        [Required(ErrorMessage = "You must enter a unit type")]

        public Nullable<int> ID_MEDIDA_PACKAGE { get; set; }

        public Nullable<int> ID_CUSTOM_UNIT { get; set; }
        public Nullable<int> QUANTITY_PACKAGE { get; set; }
        public Nullable<bool> IS_BOX { get; set; }
        public Nullable<int> QUANTITY_BOX { get; set; }
        public Nullable<int> QUANTITY { get; set; }

        public static implicit operator SupplierInsumoViewModel(SUPPLIER_INSUMO sUPPLIER)
        {
            return new SupplierInsumoViewModel
            {

                ID_SUPPLIER_INSUMO = sUPPLIER.ID_SUPPLIER_INSUMO,
                 CODE = sUPPLIER.CODE,
                 QUANTITY = sUPPLIER.QUANTITY,
                 ID_INSUMO = sUPPLIER.ID_INSUMO,
                 ID_MEDIDA_PACKAGE = sUPPLIER.ID_MEDIDA_PACKAGE,
                 ID_SUPPLIER = sUPPLIER.ID_SUPPLIER,
                 IS_BOX = sUPPLIER.IS_BOX,
                 IS_PACKAGE = sUPPLIER.IS_PACKAGE,
                 PRICE = sUPPLIER.PRICE,
                 QUANTITY_BOX = sUPPLIER.QUANTITY_BOX,
                 QUANTITY_PACKAGE = sUPPLIER.QUANTITY_PACKAGE,
                ACTIVE = true,
                 SELECTED =true,
                 NAME =sUPPLIER.NAME
            };

        }
        public static implicit operator SUPPLIER_INSUMO(SupplierInsumoViewModel sUPPLIER)
        {
            return new SUPPLIER_INSUMO
            {

                ID_SUPPLIER_INSUMO = sUPPLIER.ID_SUPPLIER_INSUMO,
                CODE = sUPPLIER.CODE,
                ID_INSUMO = sUPPLIER.ID_INSUMO,
                ID_MEDIDA_PACKAGE = sUPPLIER.ID_MEDIDA_PACKAGE,
                ID_SUPPLIER = sUPPLIER.ID_SUPPLIER,
                IS_BOX = sUPPLIER.IS_BOX,
                IS_PACKAGE = sUPPLIER.IS_PACKAGE,
                PRICE = sUPPLIER.PRICE,
                QUANTITY_BOX = sUPPLIER.QUANTITY_BOX,
                QUANTITY_PACKAGE = sUPPLIER.QUANTITY_PACKAGE,
                ACTIVE = true,
                SELECTED = true,
                NAME = sUPPLIER.NAME,
                QUANTITY  = sUPPLIER.QUANTITY

            };

        }

    }
}