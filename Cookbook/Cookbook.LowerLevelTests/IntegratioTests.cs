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
    }
}
