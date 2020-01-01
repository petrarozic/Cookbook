using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Recipes.Any())
            {
                var recipe0 = new Recipe { Name = "Burek" };
                var recipe1 = new Recipe { Name = "Lazanje" };
                var recipe2 = new Recipe { Name = "Bolonjez" };
                var recipe3 = new Recipe { Name = "Grah sa zeljem" };
                var recipe4 = new Recipe { Name = "Pizza" };
                var recipe5 = new Recipe { Name = "Rižoto" };
                var recipe6 = new Recipe { Name = "Granadir" };
                var recipe7 = new Recipe { Name = "Jaja sa špekom" };
                var recipe8 = new Recipe { Name = "Hamburger" };
                var recipe9 = new Recipe { Name = "Varivo od mahuna" };

                context.Recipes.AddRange(recipe0, recipe1, recipe2, recipe3, recipe4, recipe5, recipe6, recipe7, recipe8, recipe9);
                context.SaveChanges();
            }
        }
    }
}
