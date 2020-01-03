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

                recipe0.Ingredients = "1kg Meso";
                recipe1.Ingredients = "1kg Meso, 750g Tjestenina";
                recipe2.Ingredients = "500g Meso, 500g Tjestenina";
                recipe3.Ingredients = "450g Zelje, 40dag Grah";
                recipe4.Ingredients = "25dag Sir, 25dag Sunka, 30dag Gljive";
                recipe5.Ingredients = "500g Meso, 400g Riza";
                recipe6.Ingredients = "250g Tjestenina, 250g Krumpir";
                recipe7.Ingredients = "3kom Jaje, 100g Špek";
                recipe8.Ingredients = "250g Meso, 100g Sir";
                recipe9.Ingredients = "40dag Meso, 1kg Mahune";

                context.SaveChanges();

                recipe0.Steps = "Razvaljas tijesto. Razbacas meso. Zarolas i stavis pec.";
                recipe1.Steps = "Postavis tijesto. Stavis meso. Ponavljas postupak. Stavis peć.";
                recipe2.Steps = "Izdinstas meso sa lukom. Skuhas tijesteninu i dodas je.";
                recipe3.Steps = "Prokuhas grah. Dodas zelje i kuhas jos neko vrijeme.";
                recipe4.Steps = "Rastegnes tijesto. Poslozis sunku. Poslozis sir. Poslozis gljive. Stavis pec.";
                recipe5.Steps = "Izdinstas luk i meso. Dodas rizu i kuhas jos 15min.";
                recipe6.Steps = "Skuhas krumpir. Skuhas tijesto. Pomijesas zajedno krumpir i tijesto.";
                recipe7.Steps = "Nasijeckas spek. Sprzis spek na ulju. Dodas razmucena jaja i speces.";
                recipe8.Steps = "Prerezes pecivo. Dodas peceno meso. Dodas sir.";
                recipe9.Steps = "Prodinstas luk sa mesom. Dodas mahune i kuhas jos neko vrijeme.";

                context.SaveChanges();
            }
        }
    }
}
