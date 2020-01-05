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

                context.Ingredients.AddRange(
                    new Ingredient { Name = "Meso" },
                    new Ingredient { Name = "Mahune" },
                    new Ingredient { Name = "Sir" },
                    new Ingredient { Name = "Zelje" },
                    new Ingredient { Name = "Sunka" },
                    new Ingredient { Name = "Jaja" },
                    new Ingredient { Name = "Špek" },
                    new Ingredient { Name = "Tjestenina" },
                    new Ingredient { Name = "Krumpir" },
                    new Ingredient { Name = "Riža" },
                    new Ingredient { Name = "Gljive" },
                    new Ingredient { Name = "Grah" }
                    );
                context.SaveChanges();
                recipe0.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 1,
                        MeasuringUnit = "kg"
                    },
                };

                recipe1.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 1,
                        MeasuringUnit = "kg"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 750,
                        MeasuringUnit = "g"
                    }
                };

                recipe2.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 500,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 500,
                        MeasuringUnit = "g"
                    }
                };

                recipe3.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Zelje"),
                        Amount = 450,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Grah"),
                        Amount = 40,
                        MeasuringUnit = "dag"
                    }
                };

                recipe4.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sir"),
                        Amount = 25,
                        MeasuringUnit = "dag"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sunka"),
                        Amount = 25,
                        MeasuringUnit = "dag"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Gljive"),
                        Amount = 30,
                        MeasuringUnit = "dag"
                    }
                };

                recipe5.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 500,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Riža"),
                        Amount = 400,
                        MeasuringUnit = "g"
                    }
                };

                recipe6.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Tjestenina"),
                        Amount = 250,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Krumpir"),
                        Amount = 250,
                        MeasuringUnit = "g"
                    }
                };

                recipe7.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Jaja"),
                        Amount = 3,
                        MeasuringUnit = "kom"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Špek"),
                        Amount = 100,
                        MeasuringUnit = "g"
                    }
                };

                recipe8.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 250,
                        MeasuringUnit = "g"
                    },
                    new RecipeIngredient {
                        Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Sir"),
                        Amount = 100,
                        MeasuringUnit = "g"
                    }
                };

                recipe9.RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient { Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Meso"),
                        Amount = 40,
                        MeasuringUnit = "dag"
                    },
                    new RecipeIngredient { Ingredient = context.Ingredients.FirstOrDefault(i => i.Name == "Mahune"),
                        Amount = 1,
                        MeasuringUnit = "kg"
                    }
                };

                context.SaveChanges();

                recipe0.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Razvaljas tijesto" },
                        new Step { Order = 2, Description = "Razbacas meso" },
                        new Step { Order = 3, Description = "Zarolas i stavis pec" }
                    };
                recipe1.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Postavis tijesto" },
                        new Step { Order = 2, Description = "Stavis meso" },
                        new Step { Order = 3, Description = "Ponavljas postupak" },
                        new Step { Order = 4, Description = "Stavis peć" }
                    };
                recipe2.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Izdinstas meso sa lukom" },
                        new Step { Order = 2, Description = "Skuhas tijesteninu i dodas je" }
                    };
                recipe3.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Prokuhas grah" },
                        new Step { Order = 2, Description = "Dodas zelje i kuhas jos neko vrijeme" }
                    };
                recipe4.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Rastegnes tijesto" },
                        new Step { Order = 2, Description = "Poslozis sunku" },
                        new Step { Order = 3, Description = "Poslozis sir" },
                        new Step { Order = 4, Description = "Poslozis gljive" },
                        new Step { Order = 5, Description = "Stavis pec" }
                    };
                recipe5.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Izdinstas luk i meso" },
                        new Step { Order = 2, Description = "Dodas rizu i kuhas jos 15min" }
                    };
                recipe6.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Skuhas krumpir" },
                        new Step { Order = 2, Description = "Skuhas tijesto" },
                        new Step { Order = 3, Description = "Pomijesas zajedno krumpir i tijesto" }
                    };
                recipe7.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Nasijeckas spek" },
                        new Step { Order = 2, Description = "Sprzis spek na ulju" },
                        new Step { Order = 3, Description = "Dodas razmucena jaja i speces" }
                    };
                recipe8.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Prerezes pecivo" },
                        new Step { Order = 2, Description = "Dodas peceno meso" },
                        new Step { Order = 3, Description = "Dodas sir" }
                    };
                recipe9.Steps = new List<Step>
                    {
                        new Step { Order = 1, Description = "Prodinstas luk sa mesom" },
                        new Step { Order = 2, Description = "Dodas mahune i kuhas jos neko vrijeme" }
                    };

                context.SaveChanges();
            }
        }
    }
}
