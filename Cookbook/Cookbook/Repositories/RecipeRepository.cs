using Cookbook.Interfaces;
using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _appDbContext;

        public RecipeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Recipe> GetAllRecipe()
        {
            return _appDbContext.Recipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            return _appDbContext.Recipes
                                .Where(r => r.RecipeId == recipeId)
                                .FirstOrDefault();
        }
    }
}
