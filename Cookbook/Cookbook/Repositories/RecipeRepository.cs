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
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeRepository(AppDbContext appDbContext, IIngredientRepository ingredientRepository)
        {
            _appDbContext = appDbContext;
            _ingredientRepository = ingredientRepository;
        }

        public void AddRecipe(Recipe recipe)
        {
            recipe.RecipeIngredients = SetRecipeIngredients(recipe.RecipeIngredients.ToList());
            _appDbContext.Recipes.Add(recipe);
            _appDbContext.SaveChanges();
        }

        private ICollection<RecipeIngredient> SetRecipeIngredients(List<RecipeIngredient> recipeIngredients)
        {
            List<RecipeIngredient> setRecipeIngredients = new List<RecipeIngredient>();
            List<String> ingredientNames = _ingredientRepository.GetAllIngredientName().ToList();

            foreach (var x in recipeIngredients)
            {
                if (ingredientNames.Contains(x.Ingredient.Name))
                {
                    setRecipeIngredients.Add(
                        new RecipeIngredient
                        {
                            Ingredient = _ingredientRepository.GetIngredientByName(x.Ingredient.Name),
                            Amount = x.Amount,
                            MeasuringUnit = x.MeasuringUnit
                        });
                }
                else
                {
                    setRecipeIngredients.Add(
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient { Name = x.Ingredient.Name },
                            Amount = x.Amount,
                            MeasuringUnit = x.MeasuringUnit
                        });
                }
            }
            return setRecipeIngredients;
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
                IngredientDTO ingredientDTO = new IngredientDTO
                {
                    Amount = x.Amount,
                    MeasuringUnit = x.MeasuringUnit,
                    Name = x.Ingredient.Name
                };

                ingredientDTOs.Add(ingredientDTO);
            }
            recipeDetailDTO.Ingredients = ingredientDTOs;

            List<StepDTO> stepDTOs = new List<StepDTO>();
            foreach(var x in recipe.Steps)
            {
                StepDTO stepDTO = new StepDTO
                {
                    Order = x.Order,
                    Description = x.Description
                };

                stepDTOs.Add(stepDTO);
            }
            recipeDetailDTO.Steps = stepDTOs;

            return recipeDetailDTO;
        }
    }
}
