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
        public void getContent()
        {
            _driver.Navigate().GoToUrl("http://localhost:58883/");
            DelayForDemoVideo();
            
            Assert.Contains("Cookbook", _driver.PageSource.ToString());
            Assert.Equal("Cookbook", _driver.Title);

            var recipes = _driver.FindElement(By.Id("Recipes")).FindElements(By.ClassName("recipe"));

            var recipe = _driver.FindElement(By.Id("Recipes")).FindElement(By.ClassName("recipe"));
            Assert.NotNull(recipe);

            Assert.True(recipes.Count > 0, "No recipe is displayed on page");
        }

        private void DelayForDemoVideo(int delay = 500)
        {
            Thread.Sleep(delay);
        }
    }
}
