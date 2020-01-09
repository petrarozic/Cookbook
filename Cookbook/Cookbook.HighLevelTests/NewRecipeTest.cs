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

            var addNewIngredientButton = _driver.FindElement(By.Id("addInputsForIngredient"));
            addNewIngredientButton.Click();
            addNewIngredientButton.Click();
            DelayForDemoVideo();

            List<IngredientDTO> ingredientValues = new List<IngredientDTO>
            {
                new IngredientDTO{ Name="Juneće meso", Amount=400, MeasuringUnit="g"},
                new IngredientDTO{ Name="Crveni luk", Amount=1, MeasuringUnit="kom"},
                new IngredientDTO{ Name="Češnjeva", Amount=2, MeasuringUnit="kom"}
            };

            var ingredients = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                                    .FindElements(By.ClassName("ingredient"));
            Assert.Equal(3, ingredients.Count);

            int ingredientNum = 0;
            foreach(var x in ingredients)
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

            _driver.FindElement(
                By.XPath("//div[@id = '"+ ingredientIndex.ToString() + "' and @class = 'ingredient']//button[@id = 'deleteIngredient']")
                ).Click();

            DelayForDemoVideo();
            ingredientValues.RemoveAt(ingredientIndex);

            ingredients = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                                    .FindElements(By.ClassName("ingredient"));
            Assert.Equal(2, ingredients.Count);

            ingredientNum = 0;
            foreach (var x in ingredients)
            {
                Assert.Equal(
                    ingredientValues[ingredientNum].Name,
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]")).GetAttribute("value")
                    );
                Assert.Equal(
                    ingredientValues[ingredientNum].Amount.ToString(), 
                    x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]")).GetAttribute("value")
                    );
                Assert.Equal(
                    ingredientValues[ingredientNum].MeasuringUnit, 
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

            List<IngredientDTO> ingredientValues = new List<IngredientDTO>
            {
                new IngredientDTO{ Name="Juneće meso", Amount=400, MeasuringUnit="g"}
            };

            var ingredients = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"))
                                    .FindElements(By.ClassName("ingredient"));
            Assert.Single(ingredients);

            foreach (var x in ingredients)
            {
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]"))
                    .SendKeys(ingredientValues[0].Name);
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]"))
                    .SendKeys(ingredientValues[0].Amount.ToString());
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"))
                    .SendKeys(ingredientValues[0].MeasuringUnit);
                DelayForDemoVideo();
            }

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

            var addNewStepButton = _driver.FindElement(By.Id("addInputsForStep"));
            addNewStepButton.Click();
            addNewStepButton.Click();
            DelayForDemoVideo();

            List<StepDTO> stepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"},
                new StepDTO{ Order = 1, Description = "Oblikuj čufte"},
                new StepDTO{ Order = 1, Description = "Kratko ih prži na ulju"}
            };

            var steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                   .FindElements(By.ClassName("step"));
            Assert.Equal(3, steps.Count);

            int stepNum = 0;
            foreach (var x in steps)
            {
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"))
                    .SendKeys(stepValues[stepNum].Description);
                DelayForDemoVideo();
                stepNum++;
            }

            _driver.FindElement(
               By.XPath("//div[@id = '" + stepIndex.ToString() + "' and @class = 'step']//button[@id = 'deleteStep']")
               ).Click();

            DelayForDemoVideo();
            stepValues.RemoveAt(stepIndex);

            steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                    .FindElements(By.ClassName("step"));
            Assert.Equal(2, steps.Count);

            stepNum = 0;
            foreach (var x in steps)
            {
                Assert.Equal(
                    stepValues[stepNum].Description,
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

            List<StepDTO> stepValues = new List<StepDTO>
            {
                new StepDTO{ Order = 1, Description = "U posudi promješajte sve sastojke za čufte"}
            };

            var steps = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"))
                                   .FindElements(By.ClassName("step"));
            Assert.Single(steps);

            foreach (var x in steps)
            {
                x.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"))
                    .SendKeys(stepValues[0].Description);
                DelayForDemoVideo();
            }

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

            var addNewIngredient = _driver.FindElement(By.XPath("//button[@id='addInputsForIngredient']"));
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

            var deleteButton = element.FindElement(By.XPath(".//button[@id='deleteIngredient']"));
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

            var addNewStep = _driver.FindElement(By.XPath("//button[@id='addInputsForStep']"));
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

            var deleteButton = element.FindElement(By.XPath(".//button[@id='deleteStep']"));
            Assert.NotNull(deleteButton);
        }
    }
}
