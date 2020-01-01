using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cookbook.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Cookbook.Interfaces;
using Cookbook.Models;

namespace Cookbook.LowerLevelTests
{
    public class HomePageTest
    {
        [Fact]
        public void IndexReturnListOfRecipe()
        {
            Mock<IRecipeRepository> recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(x => x.GetAllRecipe())
                .Returns(GetAllRecipe());
            HomeController homeController = new HomeController(recipeRepository.Object);
            var result = homeController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var recipes = Assert.IsAssignableFrom<List<Recipe>>(homeController.ViewBag.Recipes);
            Assert.True(recipes.Count > 0, "Recipes from database not retrieved");
            Assert.Null(homeController.ViewBag.NoRecipe);
        }

        private IEnumerable<Recipe> GetAllRecipe()
        {
            return new List<Recipe>
            {
                new Recipe { RecipeId = 1, Name = "Ledeni vjetar" },
                new Recipe { RecipeId = 2, Name = "Madarica" },
                new Recipe { RecipeId = 3, Name = "Orahnjaca" },
                new Recipe { RecipeId = 4, Name = "Breskvice" },
                new Recipe { RecipeId = 5, Name = "Cupavac" }
            };
        }

        [Fact]
        public void IndexReturnMessageToDisplay()
        {
            Mock<IRecipeRepository> recipeRepository = new Mock<IRecipeRepository>();
            recipeRepository.Setup(x => x.GetAllRecipe())
                            .Returns(new List<Recipe>());
            HomeController homeController = new HomeController(recipeRepository.Object);
            var result = homeController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(homeController.ViewBag.Recipes);
            Assert.Equal("No recipes for display", homeController.ViewBag.NoRecipe);
        }
    }
}
