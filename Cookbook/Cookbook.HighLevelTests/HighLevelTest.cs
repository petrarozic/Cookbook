using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Xunit;

namespace Cookbook.HighLevelTests
{
    public class HighLevelTest : IDisposable
    {
        private readonly IWebDriver _driver;

        public HighLevelTest()
        {
            _driver = new ChromeDriver(Environment.CurrentDirectory);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void HomePageShouldDisplayTitle()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/");
            DelayForDemoVideo();

            Assert.Contains("Cookbook", _driver.PageSource);
            Assert.Equal("Cookbook", _driver.Title);
        }

        [Fact]
        public void HomaPageShouldDisplayRecipes()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/");
            DelayForDemoVideo();

            var recipes = _driver.FindElement(By.Id("Recipes")).FindElements(By.ClassName("recipe"));

            var recipe = _driver.FindElement(By.Id("Recipes")).FindElement(By.ClassName("recipe"));
            Assert.NotNull(recipe);

            Assert.True(recipes.Count > 0, "No recipe is displayed on page");
        }

        [Fact]
        public void ClickOnRecipeSholudDisplayRecipeDetails()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/");

            var recipes = _driver.FindElement(By.Id("Recipes")).FindElements(By.ClassName("recipe"));
            string recipeName = null;
            foreach (var r in recipes)
            {
                recipeName = r.Text;
                r.Click();
                break;
            }

            DelayForDemoVideo();

            Assert.Matches(@"http:\/\/localhost:58883\/Recipe\/[1-9]+$", _driver.Url);
            Assert.Equal("Cookbook", _driver.Title);

            string pageTitle = _driver.FindElement(By.TagName("h1")).Text;
            Assert.Contains(pageTitle, recipeName);

            Assert.Contains("Ingredients", _driver.PageSource);
            Assert.Matches("1 kg.*Meso", _driver.PageSource);
            Assert.Contains("Steps", _driver.PageSource);
            Assert.Contains("Razvaljas tijesto", _driver.PageSource);

            Assert.DoesNotContain("The requested recipe cannot be displayed", _driver.PageSource);
        }

        [Fact]
        public void RequestToDisplayNonExistingRecipe()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/-1");
            DelayForDemoVideo();

            Assert.Equal("Cookbook", _driver.Title);
            Assert.DoesNotContain("Ingredients", _driver.PageSource);
            Assert.DoesNotContain("Steps", _driver.PageSource);
            Assert.Contains("The requested recipe cannot be displayed", _driver.PageSource);
        }

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

            var recipeName = _driver.FindElement(By.XPath("//label[contains(text(), 'Recipe name')]"));
            Assert.NotNull(recipeName);
            Assert.Equal("Recipe_Name", recipeName.GetAttribute("for"));

            var inputRecipeName = _driver.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Recipe name')]/@for)]"));
            Assert.NotNull(inputRecipeName);
            Assert.Equal("text", inputRecipeName.GetAttribute("type"));
            Assert.Equal("Recipe.Name", inputRecipeName.GetAttribute("name"));

            var recipeIngredients = _driver.FindElement(By.XPath("//label[contains(text(), 'Ingredients')]"));
            Assert.NotNull(recipeIngredients);
            Assert.Equal("Ingredients", recipeIngredients.GetAttribute("for"));
            var ingredients = _driver.FindElements(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"));
            Assert.Single(ingredients);

            foreach (var i in ingredients)
            {
                var ingNameLabel = i.FindElement(By.XPath("//label[contains(text(), 'Name')]"));
                Assert.NotNull(ingNameLabel);
                Assert.Equal("Recipe_Ingredients_0__Name", ingNameLabel.GetAttribute("for"));
                var ingName = i.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Name')]/@for)]"));
                Assert.Equal("text", ingName.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[0].Name", ingName.GetAttribute("name"));

                var ingAmountLabel = i.FindElement(By.XPath("//label[contains(text(), 'Amount')]"));
                Assert.NotNull(ingAmountLabel);
                Assert.Equal("Recipe_Ingredients_0__Amount", ingAmountLabel.GetAttribute("for"));
                var ingAmount = i.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Amount')]/@for)]"));
                Assert.Equal("number", ingAmount.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[0].Amount", ingAmount.GetAttribute("name"));

                var ingMuLabel = i.FindElement(By.XPath("//label[contains(text(), 'Measuring unit')]"));
                Assert.NotNull(ingMuLabel);
                Assert.Equal("Recipe_Ingredients_0__MeasuringUnit", ingMuLabel.GetAttribute("for"));
                var ingMu = i.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"));
                Assert.Equal("text", ingMu.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[0].MeasuringUnit", ingMu.GetAttribute("name"));
            }

            var recipeSteps = _driver.FindElement(By.XPath("//label[contains(text(), 'Steps')]"));
            Assert.NotNull(recipeSteps);
            Assert.Equal("Steps", recipeSteps.GetAttribute("for"));
            var steps = _driver.FindElements(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"));
            Assert.Single(steps);
            foreach (var s in steps)
            {
                Assert.Contains("1.", s.Text);
                var stepDescLabel = s.FindElement(By.XPath("//label[contains(text(), 'Description')]"));
                Assert.NotNull(stepDescLabel);
                Assert.Equal("Recipe_Steps_0__Description", stepDescLabel.GetAttribute("for"));
                var stepDesc = s.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Description')]/@for)]"));
                Assert.Equal("text",stepDesc.GetAttribute("type"));
                Assert.Equal("Recipe.Steps[0].Description", stepDesc.GetAttribute("name"));
            }
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

            var recipeName = _driver.FindElement(By.XPath("//label[contains(text(), 'Recipe name')]"));
            Assert.NotNull(recipeName);
            Assert.Equal("Recipe_Name", recipeName.GetAttribute("for"));

            var inputRecipeName = _driver.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Recipe name')]/@for)]"));
            Assert.NotNull(inputRecipeName);
            Assert.Equal("text", inputRecipeName.GetAttribute("type"));
            Assert.Equal("Recipe.Name", inputRecipeName.GetAttribute("name"));

            var recipeIngredientsLabel = _driver.FindElement(By.XPath("//label[contains(text(), 'Ingredients')]"));
            Assert.NotNull(recipeIngredientsLabel);
            Assert.Equal("Ingredients", recipeIngredientsLabel.GetAttribute("for"));
            var ingredientsDiv = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"));
            Assert.NotNull(ingredientsDiv);
            var ingredients = ingredientsDiv.FindElements(By.ClassName("ingredient"));
            Assert.Equal(2, ingredients.Count);

            int ingredientNum = 0;
            foreach (var i in ingredients)
            {
                var ingNameLabel = i.FindElement(By.XPath(".//label[contains(text(), 'Name')]"));
                Assert.NotNull(ingNameLabel);
                Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__Name", ingNameLabel.GetAttribute("for"));
                var ingName = i.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]"));
                Assert.Equal("text", ingName.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].Name", ingName.GetAttribute("name"));

                var ingAmountLabel = i.FindElement(By.XPath(".//label[contains(text(), 'Amount')]"));
                Assert.NotNull(ingAmountLabel);
                Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__Amount", ingAmountLabel.GetAttribute("for"));
                var ingAmount = i.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]"));
                Assert.Equal("number", ingAmount.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].Amount", ingAmount.GetAttribute("name"));

                var ingMuLabel = i.FindElement(By.XPath(".//label[contains(text(), 'Measuring unit')]"));
                Assert.NotNull(ingMuLabel);
                Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__MeasuringUnit", ingMuLabel.GetAttribute("for"));
                var ingMu = i.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"));
                Assert.Equal("text", ingMu.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].MeasuringUnit", ingMu.GetAttribute("name"));
                ingredientNum++;
            }

            var recipeSteps = _driver.FindElement(By.XPath("//label[contains(text(), 'Steps')]"));
            Assert.NotNull(recipeSteps);
            Assert.Equal("Steps", recipeSteps.GetAttribute("for"));
            var steps = _driver.FindElements(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"));
            Assert.Single(steps);
            foreach (var s in steps)
            {
                Assert.Contains("1.", s.Text);
                var stepDescLabel = s.FindElement(By.XPath("//label[contains(text(), 'Description')]"));
                Assert.NotNull(stepDescLabel);
                Assert.Equal("Recipe_Steps_0__Description", stepDescLabel.GetAttribute("for"));
                var stepDesc = s.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Description')]/@for)]"));
                Assert.Equal("text", stepDesc.GetAttribute("type"));
                Assert.Equal("Recipe.Steps[0].Description", stepDesc.GetAttribute("name"));
            }
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

            var recipeName = _driver.FindElement(By.XPath("//label[contains(text(), 'Recipe name')]"));
            Assert.NotNull(recipeName);
            Assert.Equal("Recipe_Name", recipeName.GetAttribute("for"));

            var inputRecipeName = _driver.FindElement(By.XPath("//input[@id=(//label[contains(text(), 'Recipe name')]/@for)]"));
            Assert.NotNull(inputRecipeName);
            Assert.Equal("text", inputRecipeName.GetAttribute("type"));
            Assert.Equal("Recipe.Name", inputRecipeName.GetAttribute("name"));

            var recipeIngredientsLabel = _driver.FindElement(By.XPath("//label[contains(text(), 'Ingredients')]"));
            Assert.NotNull(recipeIngredientsLabel);
            Assert.Equal("Ingredients", recipeIngredientsLabel.GetAttribute("for"));
            var ingredientsDiv = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Ingredients')]/@for)]"));
            Assert.NotNull(ingredientsDiv);
            var ingredients = ingredientsDiv.FindElements(By.ClassName("ingredient"));
            Assert.Single(ingredients);

            int ingredientNum = 0;
            foreach (var i in ingredients)
            {
                var ingNameLabel = i.FindElement(By.XPath(".//label[contains(text(), 'Name')]"));
                Assert.NotNull(ingNameLabel);
                Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__Name", ingNameLabel.GetAttribute("for"));
                var ingName = i.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Name')]/@for)]"));
                Assert.Equal("text", ingName.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].Name", ingName.GetAttribute("name"));

                var ingAmountLabel = i.FindElement(By.XPath(".//label[contains(text(), 'Amount')]"));
                Assert.NotNull(ingAmountLabel);
                Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__Amount", ingAmountLabel.GetAttribute("for"));
                var ingAmount = i.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Amount')]/@for)]"));
                Assert.Equal("number", ingAmount.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].Amount", ingAmount.GetAttribute("name"));

                var ingMuLabel = i.FindElement(By.XPath(".//label[contains(text(), 'Measuring unit')]"));
                Assert.NotNull(ingMuLabel);
                Assert.Equal("Recipe_Ingredients_" + ingredientNum + "__MeasuringUnit", ingMuLabel.GetAttribute("for"));
                var ingMu = i.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Measuring unit')]/@for)]"));
                Assert.Equal("text", ingMu.GetAttribute("type"));
                Assert.Equal("Recipe.Ingredients[" + ingredientNum + "].MeasuringUnit", ingMu.GetAttribute("name"));
                ingredientNum++;
            }

            var recipeStepsLabel = _driver.FindElement(By.XPath("//label[contains(text(), 'Steps')]"));
            Assert.NotNull(recipeStepsLabel);
            Assert.Equal("Steps", recipeStepsLabel.GetAttribute("for"));
            var stepsDiv = _driver.FindElement(By.XPath("//div[@id=(//label[contains(text(), 'Steps')]/@for)]"));
            Assert.NotNull(stepsDiv);
            var steps = stepsDiv.FindElements(By.ClassName("step"));
            Assert.Equal(2, steps.Count);

            int stepNum = 0;
            foreach (var s in steps)
            {
                Assert.Contains((stepNum + 1).ToString() + ".", s.FindElement(By.XPath(".//label[contains(text(), '.')]")).Text);
                var stepDescLabel = s.FindElement(By.XPath(".//label[contains(text(), 'Description')]"));
                Assert.NotNull(stepDescLabel);
                Assert.Equal("Recipe_Steps_" + stepNum + "__Description", stepDescLabel.GetAttribute("for"));
                var stepDesc = s.FindElement(By.XPath(".//input[@id=(//label[contains(text(), 'Description')]/@for)]"));
                Assert.Equal("text", stepDesc.GetAttribute("type"));
                Assert.Equal("Recipe.Steps[" + stepNum + "].Description", stepDesc.GetAttribute("name"));
                stepNum++;
            }
        }

        private void DelayForDemoVideo(int delay = 500)
        {
            Thread.Sleep(delay);
        }
    }
}
