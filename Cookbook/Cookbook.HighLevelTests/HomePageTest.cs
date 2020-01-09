using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cookbook.HighLevelTests
{
    public class HomePageTest: HighLevelTest
    {
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
    }
}
