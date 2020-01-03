using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Interfaces;
using Cookbook.Models;
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
        [Route("Recipe/{recipeId}")]
        public IActionResult Index(int recipeId)
        {
            var recipe = _recipeRepository.GetRecipeById(recipeId);
            if (recipe != null) ViewBag.Recipe = recipe;
            else ViewBag.NoRecipe = "The requested recipe cannot be displayed";

            return View();
        }
    }
}
