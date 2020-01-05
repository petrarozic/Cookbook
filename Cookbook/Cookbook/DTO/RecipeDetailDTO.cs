using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.DTO
{
    public class RecipeDetailDTO
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public List<StepDTO> Steps { get; set; }
    }
}
