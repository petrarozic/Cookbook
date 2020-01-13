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
    [Collection("Integration test collection")]
    public class IntegratioTests: IntegrationTestBase
    {
        [Fact]
        public void RetrivesAllDataFromDB()
        {
            var context = GivenAppContext();
            var ingredientRepo = new IngredientRepository(context);
            var recipeRepo = new RecipeRepository(context, ingredientRepo);

            Recipe recipe1 = new Recipe
            {
                Name = "RetrivesAllDataFromDB1",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing11"}, Amount = 11, MeasuringUnit = "kg"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing12"}, Amount = 12, MeasuringUnit = "dag"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing13"}, Amount = 13, MeasuringUnit = "g"}
                },

                Steps = new List<Step>
                {
                    new Step{ Order = 1, Description = "Step11" },
                    new Step{ Order = 2, Description = "Step12" }
                }
            };
            Recipe recipe2 = new Recipe
            {
                Name = "RetrivesAllDataFromDB2",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing21"}, Amount = 21, MeasuringUnit = "kg"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing22"}, Amount = 22, MeasuringUnit = "dag"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing23"}, Amount = 23, MeasuringUnit = "g"}
                },

                Steps = new List<Step>
                {
                    new Step{ Order = 1, Description = "Step21" },
                    new Step{ Order = 2, Description = "Step22" }
                }
            };

            List<Recipe> recordsBeforeAdding = Assert.IsType<List<Recipe>>(recipeRepo.GetAllRecipe().ToList());

            recipeRepo.AddRecipe(recipe2);
            recipeRepo.AddRecipe(recipe1);

            List<Recipe> recordsAfterAdding = Assert.IsType<List<Recipe>>(recipeRepo.GetAllRecipe().ToList());
            Assert.Equal(2 + recordsBeforeAdding.Count(), recordsAfterAdding.Count());

            Recipe record = null;
            foreach(var x in recordsAfterAdding)
            {
                if (x.Name == recipe1.Name)
                {
                    record = x;
                    break;
                }
            }
            Assert.NotNull(record);
            Assert.Equal(recipe1.Name, record.Name);
        }

        [Fact]
        public void RetrivesRecipeById()
        {
            var context = GivenAppContext();
            var ingredientRepo = new IngredientRepository(context);
            var recipeRepo = new RecipeRepository(context, ingredientRepo);

            Recipe recipe = new Recipe
            {
                Name = "RetrivesRecipeById",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing1"}, Amount = 1, MeasuringUnit = "kg"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing2"}, Amount = 2, MeasuringUnit = "dag"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing3"}, Amount = 3, MeasuringUnit = "g"}
                },

                Steps = new List<Step>
                {
                    new Step{ Order = 1, Description = "Step1" },
                    new Step{ Order = 2, Description = "Step2" }
                }
            };

            recipeRepo.AddRecipe(recipe);

            string recipeName = "RetrivesRecipeById";
            List<IngredientDTO> ingredients= new List<IngredientDTO> {
                new IngredientDTO{Amount = 1, MeasuringUnit = "kg", Name = "Ing1" },
                new IngredientDTO{Amount = 2, MeasuringUnit = "dag", Name = "Ing2" },
                new IngredientDTO{Amount = 3, MeasuringUnit = "g", Name = "Ing3" }
            };
            List<StepDTO> steps = new List<StepDTO> {
                new StepDTO { Order = 1, Description = "Step1" },
                new StepDTO { Order = 2, Description = "Step2" }
            };

            Assert.IsType<RecipeDetailDTO>(recipeRepo.GetRecipeById(recipe.RecipeId));
        
            RecipeDetailDTO recipeDetail = recipeRepo.GetRecipeById(recipe.RecipeId);
            Assert.Equal(recipeName, recipe.Name);

            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.Equals(ingredients, recipeDetail.Ingredients);
            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.Equals(steps, recipeDetail.Steps);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(9999)]
        public void GetRecipeByIdReturnNull(int requestedId)
        {
            var context = GivenAppContext();
            var ingredientRepo = new IngredientRepository(context);
            var recipeRepo = new RecipeRepository(context, ingredientRepo);

            Assert.Null(recipeRepo.GetRecipeById(requestedId));
        }

        [Fact]
        public void AddRecipe()
        {
            var context = GivenAppContext();
            var ingredientRepo = new IngredientRepository(context);
            var recipeRepo = new RecipeRepository(context, ingredientRepo);

            var recordsBeforeAdding = recipeRepo.GetAllRecipe().Count();

            Recipe recipe = new Recipe
            {
                Name = "IntegrationTest recipe",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing1"}, Amount = 1, MeasuringUnit = "kg"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing2"}, Amount = 2, MeasuringUnit = "dag"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing3"}, Amount = 3, MeasuringUnit = "g"}
                },

                Steps = new List<Step>
                {
                    new Step{ Order = 1, Description = "Step1" },
                    new Step{ Order = 2, Description = "Step2" }
                }
            };

            recipeRepo.AddRecipe(recipe);
            var x = recipeRepo.GetAllRecipe();
            Assert.Equal(1 + recordsBeforeAdding, recipeRepo.GetAllRecipe().Count());
            Assert.NotEqual(0, recipe.RecipeId);

            var recipeDetailDTO = recipeRepo.GetRecipeById(recipe.RecipeId);
            Assert.Equal(recipe.Name, recipeDetailDTO.Name);
        }

        [Fact]
        public void ItIsNotPossibleToCreateIngredientsWithTheSameName()
        {
            var context = GivenAppContext();
            var ingredientRepo = new IngredientRepository(context);
            var recipeRepo = new RecipeRepository(context, ingredientRepo);

            var recordsBeforeAdding = ingredientRepo.GetAllIngredientName().Count();

            Recipe recipe1 = new Recipe
            {
                Name = "IntegrationTest recipe1",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "TheSameName1"}, Amount = 1, MeasuringUnit = "kg"},
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "TheSameName2"}, Amount = 2, MeasuringUnit = "dag"}
                },

                Steps = new List<Step>
                {
                    new Step{ Order = 1, Description = "TheSameName1" },
                }
            };
            Recipe recipe2 = new Recipe
            {
                Name = "IntegrationTest recipe2",
                RecipeIngredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient{ Ingredient = new Ingredient{ Name = "Ing1"}, Amount = 3, MeasuringUnit = "g"},
                },

                Steps = new List<Step>
                {
                    new Step{ Order = 1, Description = "Step1" },
                }
            };

            recipeRepo.AddRecipe(recipe1);
            recipeRepo.AddRecipe(recipe2);

            var ingredients = ingredientRepo.GetAllIngredientName();
            Assert.NotEmpty(ingredients);
            Assert.Equal(2 + recordsBeforeAdding, ingredients.Count());
        }

        [Fact]
        public void IngredientRepositoryShould()
        {
            var context = GivenAppContext();
            var ingredientRepo = new IngredientRepository(context);

            var recordsBeforeAdding = Assert.IsType<List<String>>(ingredientRepo.GetAllIngredientName().ToList()).Count();

            List<string> listOfNames = new List<string>
            {
                "IngredientRepositoryShould1",
                "IngredientRepositoryShould2",
                "IngredientRepositoryShould3",
                "IngredientRepositoryShould4",
                "IngredientRepositoryShould5"
            };

            foreach (var x in listOfNames)
            {
                context.Ingredients.Add(new Ingredient { Name = x });
            }
            context.SaveChanges();

            var recordsAfterAdding = Assert.IsType<List<String>>(ingredientRepo.GetAllIngredientName().ToList());
            foreach(var x in listOfNames)
            {
                Assert.Contains(x, recordsAfterAdding);
            }
            Assert.Equal(listOfNames.Count() + recordsBeforeAdding, recordsAfterAdding.Count());

            var ingredient = Assert.IsType<Ingredient>(ingredientRepo.GetIngredientByName(listOfNames[2]));
            Assert.Equal(listOfNames[2], ingredient.Name);
            Assert.NotEqual(0, ingredient.IngredientId);
        }
    }
}
