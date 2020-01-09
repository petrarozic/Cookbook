using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cookbook.HighLevelTests
{
    public class RecipePageTest: HighLevelTest
    {
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
    }
}
