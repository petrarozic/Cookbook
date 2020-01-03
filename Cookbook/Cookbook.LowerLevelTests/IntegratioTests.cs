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
            string recipeIngredients = "1kg Meso";
            string recipeSteps = "Razvaljas tijesto. Razbacas meso. Zarolas i stavis pec.";

            Assert.IsType<Recipe>(repo.GetRecipeById(1));

            Recipe recipe = repo.GetRecipeById(1);
            Assert.Equal(recipe.RecipeId, recipeId);
            Assert.Equal(recipe.Name, recipeName);
            Assert.Equal(recipe.Ingredients, recipeIngredients);
            Assert.Equal(recipe.Steps, recipeSteps);
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
