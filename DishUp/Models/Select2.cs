using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Balwant.Models
{


    //Extra classes to format the results the way the select2 dropdown wants them
    public class Select2OptionModel
    {

        public string id { get; set; }

        public string text { get; set; }

    }

    public class Select2PagedResult
    {

        public int Total { get; set; }

        public List<Select2OptionModel> Results { get; set; }

    }

}