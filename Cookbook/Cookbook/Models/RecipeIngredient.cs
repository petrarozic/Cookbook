using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }

        public int Amount { get; set; }
        public string MeasuringUnit { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
