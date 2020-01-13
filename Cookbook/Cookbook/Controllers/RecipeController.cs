using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.DTO;
using Cookbook.Interfaces;
using Cookbook.Models;
using Cookbook.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cookbook.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: /<controller>/
        [Route("Recipe/{recipeId:int}")]
        public IActionResult Index(int recipeId)
        {
            var recipe = _recipeRepository.GetRecipeById(recipeId);
            if (recipe != null) ViewBag.Recipe = recipe;
            else ViewBag.NoRecipe = "The requested recipe cannot be displayed";

            return View();
        }

        public IActionResult NewRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipe(RecipeViewModel recipeViewModel)
        {
            Recipe recipe = ConvertRecipeDetailDTOToRecipe(recipeViewModel.Recipe);
            _recipeRepository.AddRecipe(recipe);

            return RedirectToAction("Index", "Recipe", new { recipeId = recipe.RecipeId });
        }

        private Recipe ConvertRecipeDetailDTOToRecipe(RecipeDetailDTO recipeDetailDTO)
        {
            Recipe recipe = new Recipe { Name = recipeDetailDTO.Name };

            recipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var x in recipeDetailDTO.Ingredients)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    Ingredient = new Ingredient() { Name = x.Name },
                    Amount = x.Amount,
                    MeasuringUnit = x.MeasuringUnit
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }

            recipe.Steps = new List<Step>();
            int orderNum = 0;
            foreach (var x in recipeDetailDTO.Steps)
            {
                orderNum++;
                Step step = new Step()
                {
                    Order = orderNum,
                    Description = x.Description
                };
                recipe.Steps.Add(step);
            }

            return recipe;
        }
    }
}
