using Cookbook.DTO;
using Cookbook.Interfaces;
using Cookbook.Models;
using Microsoft.EntityFrameworkCore;
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

        public RecipeDetailDTO GetRecipeById(int recipeId)
        {
            Recipe recipe= _appDbContext.Recipes
                                .Where(r => r.RecipeId == recipeId)
                                .Include(r => r.RecipeIngredients)
                                    .ThenInclude(r => r.Ingredient)
                                .Include(r => r.Steps)
                                .FirstOrDefault();

            if (recipe == null) return null;

            RecipeDetailDTO recipeDetailDTO = new RecipeDetailDTO();
            recipeDetailDTO.RecipeId = recipe.RecipeId;
            recipeDetailDTO.Name = recipe.Name;

            List<IngredientDTO> ingredientDTOs = new List<IngredientDTO>();
            foreach(var x in recipe.RecipeIngredients)
            {
                IngredientDTO ingredientDTO = new IngredientDTO();
                ingredientDTO.Amount = x.Amount;
                ingredientDTO.MeasuringUnit = x.MeasuringUnit;
                ingredientDTO.Name = x.Ingredient.Name;

                ingredientDTOs.Add(ingredientDTO);
            }
            recipeDetailDTO.Ingredients = ingredientDTOs;

            List<StepDTO> stepDTOs = new List<StepDTO>();
            foreach(var x in recipe.Steps)
            {
                StepDTO stepDTO = new StepDTO();
                stepDTO.Order = x.Order;
                stepDTO.Description = x.Description;

                stepDTOs.Add(stepDTO);
            }
            recipeDetailDTO.Steps = stepDTOs;

            return recipeDetailDTO;
        }
    }
}
