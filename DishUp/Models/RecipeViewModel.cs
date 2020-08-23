using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class RecipeViewModel
    {
    }

    public class RecipeDetailViewModel
    {
        public int ID_RECIPE { get; set; }

        public IEnumerable<RECIPE_DETAIL> details { get; set; }
    }
}