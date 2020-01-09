using Cookbook.Controllers;
using Cookbook.DTO;
using Cookbook.Interfaces;
using Cookbook.Models;
using Cookbook.ViewModels;
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

            int recipeId = 1;
            string recipeName = "Burek";
            List<IngredientDTO> ingredients = new List<IngredientDTO> {
                new IngredientDTO{Amount = 1, MeasuringUnit = "kg", Name = "Meso" }
            };
            List<StepDTO> steps = new List<StepDTO> {
                new StepDTO { Order = 1, Description = "Razvaljas tijesto" },
                new StepDTO { Order = 2, Description = "Razbacas meso" },
                new StepDTO { Order = 3, Description = "Zarolas i stavis pec" }
            };

            recipeRepository.Setup(x => x.GetRecipeById(recipeId))
                            .Returns(new RecipeDetailDTO
                            {
                                RecipeId = recipeId,
                                Name = recipeName,
                                Ingredients = ingredients,
                                Steps = steps
                            });

            RecipeController recipeController = new RecipeController(recipeRepository.Object);
            var result = recipeController.Index(recipeId);
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<RecipeDetailDTO>(recipeController.ViewBag.Recipe);
            Assert.IsType<string>(recipeController.ViewBag.Recipe.Name);
            Assert.IsType<int>(recipeController.ViewBag.Recipe.RecipeId);
            Assert.IsType<List<IngredientDTO>>(recipeController.ViewBag.Recipe.Ingredients);
            Assert.IsType<List<StepDTO>>(recipeController.ViewBag.Recipe.Steps);

            Assert.Equal(recipeName, recipeController.ViewBag.Recipe.Name);
            Assert.Equal(recipeId, recipeController.ViewBag.Recipe.RecipeId);
            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.Equals(ingredients, recipeController.ViewBag.Recipe.Ingredients);
            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.Equals(steps, recipeController.ViewBag.Recipe.Steps);

            Assert.Null(recipeController.ViewBag.NoRecipe);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public void ReturnErrorMessage(int requestedId)
        {
            Mock<IRecipeRepository> recipeRepository = new Mock<IRecipeRepository>();

            int recipeId = 1;
            string recipeName = "Burek";
            List<IngredientDTO> ingredients = new List<IngredientDTO> {
                new IngredientDTO{Amount = 1, MeasuringUnit = "kg", Name = "Meso" }
            };
            List<StepDTO> steps = new List<StepDTO> {
                new StepDTO { Order = 1, Description = "Razvaljas tijesto" },
                new StepDTO { Order = 2, Description = "Razbacas meso" },
                new StepDTO { Order = 3, Description = "Zarolas i stavis pec" }
            };

            recipeRepository.Setup(x => x.GetRecipeById(recipeId))
                            .Returns(new RecipeDetailDTO
                            {
                                RecipeId = recipeId,
                                Name = recipeName,
                                Ingredients = ingredients,
                                Steps = steps
                            });

            RecipeController recipeController = new RecipeController(recipeRepository.Object);

            var result = recipeController.Index(requestedId);
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Null(recipeController.ViewBag.Recipe);
            Assert.Equal("The requested recipe cannot be displayed", recipeController.ViewBag.NoRecipe);
        }

        [Fact]
        public void NewRecipeShould()
        {
            Mock<IRecipeRepository> recipeRepository = new Mock<IRecipeRepository>();
            RecipeController recipeController = new RecipeController(recipeRepository.Object);
            var result = recipeController.NewRecipe();
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
