using Cookbook.DTO;
using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Interfaces
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipe();
        RecipeDetailDTO GetRecipeById(int recipeId);
    }
}
