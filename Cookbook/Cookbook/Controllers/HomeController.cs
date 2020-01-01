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
    public class HomeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public HomeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var recipes = _recipeRepository.GetAllRecipe().ToList();
            if (recipes.Any()) ViewBag.Recipes = recipes;
            else ViewBag.NoRecipe = "No recipes for display";

            return View();
        }
    }
}
