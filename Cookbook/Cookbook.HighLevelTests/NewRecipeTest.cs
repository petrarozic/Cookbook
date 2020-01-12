using Cookbook.DTO;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cookbook.HighLevelTests
{
    public class NewRecipeTest: HighLevelTest
    {
        [Fact]
        public void DisplayNewRecipeForm()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/");
            DelayForDemoVideo();
            _driver.FindElement(By.XPath("//a[@href='/Recipe/NewRecipe']")).Click();
            DelayForDemoVideo();

            Assert.Equal("Cookbook", _driver.Title);
            Assert.Contains("Create new recipe", _driver.PageSource);
        }

        [Fact]
        public void FindBasicElementsInNewRecipeForm()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            TestElementsInForm(1, 1);
        }

        [Fact]
        public void AddNewIngredient()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var addNewIngredientButton = _driver.FindElement(By.Id("addInputsForIngredient"));
            addNewIngredientButton.Click();
            DelayForDemoVideo();

            TestElementsInForm(2, 1);
        }

        [Fact]
        public void AddNewStep()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var addNewStepButton = _driver.FindElement(By.Id("addInputsForStep"));
            Assert.NotNull(addNewStepButton);
            addNewStepButton.Click();
            DelayForDemoVideo();

            TestElementsInForm(1, 2);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteIngredient(int ingredientIndex)
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var ingredients = SetIngredients(3);
            Assert.Equal(3, ingredients.Count);

            _driver.FindElement(
                By.XPath("//div[@id = '"+ ingredientIndex.ToString() + "' and @class = 'ingredient']//button[@id = 'deleteIngredient']")
                ).Click();
            DelayForDemoVideo();

            List<IngredientDTO> resultIngredientValues = new List<IngredientDTO>
            {
                new IngredientDTO{ Name="Juneće meso", Amount=400, MeasuringUnit="g"},
                new IngredientDTO{ Name="Crveni luk", Amount=1, MeasuringUnit="kom"},
                new IngredientDTO{ Name="Češnjeva", Amount=2, MeasuringUnit="kom"}
            };
            resultIngredientValues.RemoveAt(ingredientIndex);

            ingredients = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                                    .FindElements(By.ClassName("ingredient"));
            Assert.Equal(2, ingredients.Count);

            var ingredientNum = 0;
            foreach (var x in ingredients)
            {
                Assert.Equal(
                    resultIngredientValues[ingredientNum].Name,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]")).GetAttribute("value")
                    );
                Assert.Equal(
                    resultIngredientValues[ingredientNum].Amount.ToString(), 
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]")).GetAttribute("value")
                    );
                Assert.Equal(
                    resultIngredientValues[ingredientNum].MeasuringUnit, 
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]")).GetAttribute("value")
                    );

                ingredientNum++;
            }

            TestElementsInForm(2, 1);
        }

        [Fact]
        public void DeleteLastIngredient()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var ingredients = SetIngredients(1);
            Assert.Single(ingredients);

            _driver.FindElement(
                By.XPath("//div[@id = '0' and @class = 'ingredient']//button[@id = 'deleteIngredient']")
                ).Click();

            ingredients = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                                    .FindElements(By.ClassName("ingredient"));
            Assert.Single(ingredients);

            foreach (var x in ingredients)
            {
                Assert.Equal(
                    "",
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]")).GetAttribute("value")
                    );
                Assert.Equal(
                    "", 
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]")).GetAttribute("value")
                    );
                Assert.Equal(
                    "",
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]")).GetAttribute("value")
                    );
            }

            TestElementsInForm(1, 1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteStep(int stepIndex)
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var steps = SetSteps(3);
            Assert.Equal(3, steps.Count);

            _driver.FindElement(
               By.XPath("//div[@id = '" + stepIndex.ToString() + "' and @class = 'step']//button[@id = 'deleteStep']")
               ).Click();
            DelayForDemoVideo();

            List<StepDTO> resultStepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"},
                new StepDTO{ Order = 2, Description = "Oblikuj čufte"},
                new StepDTO{ Order = 3, Description = "Kratko ih prži na ulju"}
            };
            resultStepValues.RemoveAt(stepIndex);

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Equal(2, steps.Count);

            var stepNum = 0;
            foreach (var x in steps)
            {
                Assert.Equal(
                    resultStepValues[stepNum].Description,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]")).GetAttribute("value")
                    );
                stepNum++;
            }

            TestElementsInForm(1, 2);
        }

        [Fact]
        public void DeleteLastStep()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var steps = SetSteps(1);
            Assert.Single(steps);

            _driver.FindElement(
               By.XPath("//div[@id = '0' and @class = 'step']//button[@id = 'deleteStep']")
               ).Click();
            DelayForDemoVideo();

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Single(steps);

            foreach (var x in steps)
            {
                Assert.Equal(
                    "",
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]")).GetAttribute("value")
                    );
            }
            TestElementsInForm(1, 1);
        }

        [Fact]
        public void MoveUpStep()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var steps = SetSteps(3);
            Assert.Equal(3, steps.Count);

            _driver.FindElement(
               By.XPath("//div[@id = '1' and @class = 'step']//button[@id = 'moveUpStep']")
               ).Click();
            DelayForDemoVideo();

            List<StepDTO> resultStepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "Oblikuj čufte"},
                new StepDTO{ Order = 2, Description = "U posudi promješajte sve sastojke za čufte"},
                new StepDTO{ Order = 3, Description = "Kratko ih prži na ulju"}
            };

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Equal(3, steps.Count);

            int stepNum = 0;
            foreach (var x in steps)
            {
                Assert.Equal(
                    resultStepValues[stepNum].Description,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]")).GetAttribute("value")
                    );
                stepNum++;
            }

            TestElementsInForm(1, 3);
        }

        [Fact]
        public void FirstStepMoveUp()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var steps = SetSteps(1);
            Assert.Single(steps);

            _driver.FindElement(
               By.XPath("//div[@id = '0' and @class = 'step']//button[@id = 'moveUpStep']")
               ).Click();
            DelayForDemoVideo();

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Single(steps);

            List<StepDTO> resultStepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"}
            };

            foreach (var x in steps)
            {
                Assert.Equal(
                    resultStepValues[0].Description,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]")).GetAttribute("value")
                    );
            }

            TestElementsInForm(1, 1);
        }

        [Fact]
        public void MoveDownStep()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var steps = SetSteps(3);
            Assert.Equal(3, steps.Count);

            _driver.FindElement(
               By.XPath("//div[@id = '1' and @class = 'step']//button[@id = 'moveDownStep']")
               ).Click();
            DelayForDemoVideo();

            List<StepDTO> resultStepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"},
                new StepDTO{ Order = 2, Description = "Kratko ih prži na ulju"},
                new StepDTO{ Order = 3, Description = "Oblikuj čufte"}
            };

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Equal(3, steps.Count);

            int stepNum = 0;
            foreach (var x in steps)
            {
                Assert.Equal(
                    resultStepValues[stepNum].Description,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]")).GetAttribute("value")
                    );
                stepNum++;
            }

            TestElementsInForm(1, 3);
        }

        [Fact]
        public void LastStepMoveDown()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            var steps = SetSteps(2);
            Assert.Equal(2, steps.Count);

            _driver.FindElement(
               By.XPath("//div[@id = '1' and @class = 'step']//button[@id = 'moveDownStep']")
               ).Click();
            DelayForDemoVideo();

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Equal(2, steps.Count);

            List<StepDTO> resultStepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"},
                new StepDTO{ Order = 2, Description = "Oblikuj čufte"}
            };

            int stepNum = 0;
            foreach (var x in steps)
            {
                Assert.Equal(
                    resultStepValues[stepNum].Description,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]")).GetAttribute("value")
                    );
                stepNum++;
            }

            TestElementsInForm(1, 2);
        }

        [Fact]
        public void CancelForm()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            _driver.FindElement(By.XPath("//a[@href='/' and contains(text(), 'Cancel')]")).Click();
            DelayForDemoVideo();

            Assert.Equal("http://localhost:58883/", _driver.Url);
        }

        [Fact]
        public void ResetForm()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            _driver.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Recipe name')]/@for)]"))
                .SendKeys("Čufte u paradajiz sosu");

            var ingredients = SetIngredients(3);
            Assert.Equal(3, ingredients.Count);

            var steps = SetSteps(3);
            Assert.Equal(3, steps.Count);

            _driver.FindElement(By.XPath("//a[@href='/Recipe/NewRecipe' and contains(text(), 'Reset')]")).Click();
            DelayForDemoVideo();

            TestElementsInForm(1, 1);

            Assert.Equal("",
                _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                   .FindElement(By.ClassName("step"))
                                   .FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"))
                                   .GetAttribute("value")
                );

            var ingredient = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                        .FindElement(By.ClassName("ingredient"));

            Assert.Equal("",
                ingredient.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]"))
                    .GetAttribute("value")
                );
            Assert.Equal("",
                ingredient.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]"))
                    .GetAttribute("value")
                );
           Assert.Equal("",
                ingredient.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"))
                    .GetAttribute("value")
                );
        }

        [Fact]
        public void SaveNewRecipe()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/NewRecipe");
            DelayForDemoVideo();

            _driver.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Recipe name')]/@for)]"))
                .SendKeys("Čufte u paradajiz sosu");
            var ingredients = SetIngredients();
            var steps = SetSteps();

            var form = _driver.FindElement(By.TagName("form"));
            Assert.Equal("post", form.GetAttribute("method"));
            Assert.Matches(@".*\/Recipe\/AddRecipe", form.GetAttribute("action"));

            _driver.FindElement(By.XPath("//input[@type='submit' and @value = 'Add recipe']")).Click();
            DelayForDemoVideo();

            Assert.Matches(@"http:\/\/localhost:58883\/Recipe\/[1-9]+$", _driver.Url);
            //TODO..
        }

        private void TestElementsInForm(int expectedNumOfIngredients, int expecteNumOfSteps)
        {
            TestRecipeNameElements();

            var ingredients = TestRecipeIngredientsElements();
            Assert.Equal(expectedNumOfIngredients, ingredients.Count);

            int ingredientNum = 0;
            foreach (var i in ingredients)
            {
                TestIngredientElements(i, ingredientNum);
                ingredientNum++;
            }

            var steps = TestRecipeStepsElements();
            Assert.Equal(expecteNumOfSteps, steps.Count);

            int stepNum = 0;
            foreach (var s in steps)
            {
                TestStepElements(s, stepNum);
                stepNum++;
            }

            _driver.FindElement(By.XPath("//a[@href='/' and contains(text(), 'Cancel')]"));
            _driver.FindElement(By.XPath("//a[@href='/Recipe/NewRecipe' and contains(text(), 'Reset')]"));
        }

        private void TestRecipeNameElements()
        {
            var recipeName = _driver.FindElement(By.XPath("//label[contains(text(), 'Recipe name')]"));
            Assert.NotNull(recipeName);
            Assert.Equal("Recipe_Name", recipeName.GetAttribute("for"));

            var inputRecipeName = _driver.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Recipe name')]/@for)]"));
            Assert.NotNull(inputRecipeName);
            Assert.Equal("text", inputRecipeName.GetAttribute("type"));
            Assert.Equal("Recipe.Name", inputRecipeName.GetAttribute("name"));
        }

        private IReadOnlyCollection<IWebElement> TestRecipeIngredientsElements()
        {
            var recipeIngredientsLabel = _driver.FindElement(By.XPath("//label[contains(text(), 'Ingredients')]"));
            Assert.NotNull(recipeIngredientsLabel);
            Assert.Equal("Ingredients", recipeIngredientsLabel.GetAttribute("for"));
            var ingredientsDiv = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"));
            Assert.NotNull(ingredientsDiv);
            var ingredients = ingredientsDiv.FindElements(By.ClassName("ingredient"));

            var addNewIngredient = _driver.FindElement(By.XPath("//button[@id='addInputsForIngredient' and @type='button']"));
            Assert.NotNull(addNewIngredient);

            return ingredients;
        }

        private void TestIngredientElements(IWebElement element, int ingredientNum)
        {
            Assert.Equal(ingredientNum.ToString(), element.GetAttribute("id"));

            var ingNameLabel = element.FindElement(By.XPath(".//label[contains(text(), 'Name')]"));
            Assert.NotNull(ingNameLabel);
            Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__Name", ingNameLabel.GetAttribute("for"));
            var ingName = element.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]"));
            Assert.Equal("text", ingName.GetAttribute("type"));
            Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].Name", ingName.GetAttribute("name"));

            var ingAmountLabel = element.FindElement(By.XPath(".//label[contains(text(), 'Amount')]"));
            Assert.NotNull(ingAmountLabel);
            Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__Amount", ingAmountLabel.GetAttribute("for"));
            var ingAmount = element.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]"));
            Assert.Equal("number", ingAmount.GetAttribute("type"));
            Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].Amount", ingAmount.GetAttribute("name"));

            var ingMuLabel = element.FindElement(By.XPath(".//label[contains(text(), 'Measuring unit')]"));
            Assert.NotNull(ingMuLabel);
            Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__MeasuringUnit", ingMuLabel.GetAttribute("for"));
            var ingMu = element.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"));
            Assert.Equal("text", ingMu.GetAttribute("type"));
            Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].MeasuringUnit", ingMu.GetAttribute("name"));

            var deleteButton = element.FindElement(By.XPath(".//button[@id='deleteIngredient' and @type='button']"));
            Assert.NotNull(deleteButton);
        }

        private IReadOnlyCollection<IWebElement> TestRecipeStepsElements()
        {
            var recipeStepsLabel = _driver.FindElement(By.XPath("//label[contains(text(), 'Steps')]"));
            Assert.NotNull(recipeStepsLabel);
            Assert.Equal("Steps", recipeStepsLabel.GetAttribute("for"));
            var stepsDiv = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"));
            Assert.NotNull(stepsDiv);
            var steps = stepsDiv.FindElements(By.ClassName("step"));

            var addNewStep = _driver.FindElement(By.XPath("//button[@id='addInputsForStep' and @type='button']"));
            Assert.NotNull(addNewStep);

            return steps;
        }

        private void TestStepElements(IWebElement element, int stepNum)
        {
            Assert.Equal(stepNum.ToString(), element.GetAttribute("id"));

            Assert.Contains((stepNum + 1).ToString() + ".", element.FindElement(By.XPath(".//label[contains(text(), '.')]")).Text);
            var stepDescLabel = element.FindElement(By.XPath(".//label[contains(text(), 'Description')]"));
            Assert.NotNull(stepDescLabel);
            Assert.Equal("Recipe_Steps_" + stepNum + "__Description", stepDescLabel.GetAttribute("for"));
            var stepDesc = element.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"));
            Assert.Equal("text", stepDesc.GetAttribute("type"));
            Assert.Equal("Recipe.Steps[" + stepNum + "].Description", stepDesc.GetAttribute("name"));

            var deleteButton = element.FindElement(By.XPath(".//button[@id='deleteStep' and @type='button']"));
            Assert.NotNull(deleteButton);

            var moveUpButton = element.FindElement(By.XPath(".//button[@id='moveUpStep' and @type='button']"));
            Assert.NotNull(moveUpButton);

            var moveDownButton = element.FindElement(By.XPath(".//button[@id='moveDownStep'  and @type='button']"));
            Assert.NotNull(moveDownButton);
        }

        private IReadOnlyCollection<IWebElement> SetIngredients(int ingredientNum = 9)
        {
            if (ingredientNum > 9) throw new System.ArgumentException("Number of ingredients cannot be greater than 9", "ingredientNum");

            List<IngredientDTO> ingredientValues = new List<IngredientDTO>
            {
                new IngredientDTO{ Name="Juneće meso", Amount=400, MeasuringUnit="g"},
                new IngredientDTO{ Name="Crveni luk", Amount=1, MeasuringUnit="kom"},
                new IngredientDTO{ Name="Češnjeva", Amount=2, MeasuringUnit="kom"},
                new IngredientDTO{ Name="Stari kruh", Amount=1, MeasuringUnit="kom"},
                new IngredientDTO{ Name="Jaje", Amount=1, MeasuringUnit="kom"},
                new IngredientDTO{ Name="Paradajiz sos", Amount=400, MeasuringUnit="ml"},
                new IngredientDTO{ Name="Vode", Amount=400, MeasuringUnit="ml"},
                new IngredientDTO{ Name="Brašna", Amount=2, MeasuringUnit="žlice"},
                new IngredientDTO{ Name="Mlijevena paprika", Amount=2, MeasuringUnit="žlice"},
            };

            var addNewIngredientButton = _driver.FindElement(By.Id("addInputsForIngredient"));
            for(var x = ingredientNum -1; x > 0; x--) addNewIngredientButton.Click();
            DelayForDemoVideo();

            var ingredients = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                                    .FindElements(By.ClassName("ingredient"));

            ingredientNum = 0;
            foreach (var x in ingredients)
            {
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]"))
                    .SendKeys(ingredientValues[ingredientNum].Name);
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]"))
                    .SendKeys(ingredientValues[ingredientNum].Amount.ToString());
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"))
                    .SendKeys(ingredientValues[ingredientNum].MeasuringUnit);
                DelayForDemoVideo();
                ingredientNum++;
            }

            return ingredients;
        }

        private IReadOnlyCollection<IWebElement> SetSteps(int stepNum = 6)
        {
            if (stepNum > 6) throw new System.ArgumentException("Number of steps cannot be greater than 6", "stepNum");
            List<StepDTO> stepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"},
                new StepDTO{ Order = 2, Description = "Oblikuj čufte"},
                new StepDTO{ Order = 3, Description = "Kratko ih prži na ulju"},
                new StepDTO{ Order = 4, Description = "U drugoj posudi prokuhaj sastojke za sos"},
                new StepDTO{ Order = 5, Description = "Pržene čufte dodajte u sos"},
                new StepDTO{ Order = 6, Description = "Dodati začine po volji"}
            };

            var addNewStepButton = _driver.FindElement(By.Id("addInputsForStep"));
            for(var x = stepNum - 1; x > 0; x--) addNewStepButton.Click();
            DelayForDemoVideo();

            var steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                   .FindElements(By.ClassName("step"));

            stepNum = 0;
            foreach (var x in steps)
            {
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"))
                    .SendKeys(stepValues[stepNum].Description);
                DelayForDemoVideo();
                stepNum++;
            }

            return steps;
        }
    }
}
