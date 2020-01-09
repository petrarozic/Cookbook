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
            Assert.NotNull(addNewIngredientButton);
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
            return ingredients;
        }

        private void TestIngredientElements(IWebElement element, int ingredientNum)
        {
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
        }

        private IReadOnlyCollection<IWebElement> TestRecipeStepsElements()
        {
            var recipeStepsLabel = _driver.FindElement(By.XPath("//label[contains(text(), 'Steps')]"));
            Assert.NotNull(recipeStepsLabel);
            Assert.Equal("Steps", recipeStepsLabel.GetAttribute("for"));
            var stepsDiv = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"));
            Assert.NotNull(stepsDiv);
            var steps = stepsDiv.FindElements(By.ClassName("step"));
            return steps;
        }

        private void TestStepElements(IWebElement element, int stepNum)
        {
            Assert.Contains((stepNum + 1).ToString() + ".", element.FindElement(By.XPath(".//label[contains(text(), '.')]")).Text);
            var stepDescLabel = element.FindElement(By.XPath(".//label[contains(text(), 'Description')]"));
            Assert.NotNull(stepDescLabel);
            Assert.Equal("Recipe_Steps_" + stepNum + "__Description", stepDescLabel.GetAttribute("for"));
            var stepDesc = element.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"));
            Assert.Equal("text", stepDesc.GetAttribute("type"));
            Assert.Equal("Recipe.Steps[" + stepNum + "].Description", stepDesc.GetAttribute("name"));
        }
    }
}
