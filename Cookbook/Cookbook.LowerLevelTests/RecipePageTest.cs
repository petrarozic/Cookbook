using Cookbook.Controllers;
using Cookbook.Interfaces;
using Cookbook.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cookbook.LowerLevelTests
{
    public class RecipePageTest
    {
        [Fact]
        public void ReturnRecipeDetails()
        {
            Mock<IRecipeRepository> recipeRepository = new Mock<IRecipeRepository>();
            string recipeName = "Burek";
            int recipeId = 1;
            string ingredients = "1kg Meso";
            string steps = "Razvaljas tijesto. Razbacas meso. Zarolas i stavis pec.";

            recipeRepository.Setup(x => x.GetRecipeById(recipeId))
                            .Returns(new Recipe { Name = recipeName, RecipeId = recipeId,
                                                  Ingredients = ingredients, Steps = steps});
            RecipeController recipeController = new RecipeController(recipeRepository.Object);
            var result = recipeController.Index(recipeId);
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<Recipe>(recipeController.ViewBag.Recipe);
            Assert.IsType<string>(recipeController.ViewBag.Recipe.Name);
            Assert.IsType<int>(recipeController.ViewBag.Recipe.RecipeId);
            Assert.IsType<string>(recipeController.ViewBag.Recipe.Ingredients);
            Assert.IsType<string>(recipeController.ViewBag.Recipe.Steps);

            Assert.Equal(recipeController.ViewBag.Recipe.Name, recipeName);
            Assert.Equal(recipeController.ViewBag.Recipe.RecipeId, recipeId);
            Assert.Equal(recipeController.ViewBag.Recipe.Ingredients, ingredients);
            Assert.Equal(recipeController.ViewBag.Recipe.Steps, steps);

            Assert.Null(recipeController.ViewBag.NoRecipe);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public void ReturnErrorMessage(int requestedId)
        {
            Mock<IRecipeRepository> recipeRepository = new Mock<IRecipeRepository>();
            string recipeName = "Burek";
            int recipeId = 1;
            recipeRepository.Setup(x => x.GetRecipeById(recipeId))
                            .Returns(new Recipe { Name = recipeName, RecipeId = recipeId });
            RecipeController recipeController = new RecipeController(recipeRepository.Object);

            var result = recipeController.Index(requestedId);
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Null(recipeController.ViewBag.Recipe);
            Assert.Equal("The requested recipe cannot be displayed", recipeController.ViewBag.NoRecipe);
        }
    }
}
