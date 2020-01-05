using Cookbook.DTO;
using Cookbook.Models;
using Cookbook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Cookbook.LowerLevelTests
{
    public class IntegratioTests: IntegrationTestBase
    {
        [Fact]
        public void RetrivesAllDataFromDB()
        {
            var context = GivenAppContext();
            var repo = new RecipeRepository(context);

            List<Recipe> records = repo.GetAllRecipe().ToList();

            Assert.Equal(10, records.Count);
            Assert.True(ListOfRecipeContains("Burek", records));
            records[0].RecipeId= 1;
            records[0].Name = "Burek";
            records[0].RecipeIngredients = null;
            records[0].Steps = null;
        }

        private bool ListOfRecipeContains(string name, List<Recipe> records)
        {
            foreach (var r in records)
                if (r.Name.Equals(name)) return true;
            return false;
        }

        [Fact]
        public void RetrivesRecipeById()
        {
            var context = GivenAppContext();
            var repo = new RecipeRepository(context);

            int recipeId = 1;
            string recipeName = "Burek";
            List<IngredientDTO> ingredients= new List<IngredientDTO> {
                new IngredientDTO{Amount = 1, MeasuringUnit = "kg", Name = "Meso" }
            };
            List<StepDTO> steps = new List<StepDTO> {
                new StepDTO { Order = 1, Description = "Razvaljas tijesto" },
                new StepDTO { Order = 2, Description = "Razbacas meso" },
                new StepDTO { Order = 3, Description = "Zarolas i stavis pec" }
            };

            Assert.IsType<RecipeDetailDTO>(repo.GetRecipeById(1));

            RecipeDetailDTO recipe = repo.GetRecipeById(1);
            Assert.Equal(recipeId, recipe.RecipeId);
            Assert.Equal(recipeName, recipe.Name);

            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.Equals(ingredients, recipe.Ingredients);
            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.Equals(steps, recipe.Steps);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(9999)]
        public void GetRecipeByIdReturnNull(int requestedId)
        {
            var context = GivenAppContext();
            var repo = new RecipeRepository(context);

            Assert.Null(repo.GetRecipeById(requestedId));
        }
    }
}
