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
            Assert.Contains("1kg Meso", _driver.PageSource);
            Assert.Contains("Steps", _driver.PageSource);
            Assert.Contains("Razvaljas tijesto", _driver.PageSource);

            Assert.DoesNotContain("The requested recipe cannot be displayed", _driver.PageSource);

            _driver.Navigate().GoToUrl("http://localhost:58883/Recipe/-1");
            DelayForDemoVideo();
            Assert.Equal("Cookbook", _driver.Title);
            Assert.DoesNotContain("Ingredients", _driver.PageSource);
            Assert.DoesNotContain("Steps", _driver.PageSource);
            Assert.Contains("The requested recipe cannot be displayed", _driver.PageSource);

            _driver.Navigate().GoToUrl("http://localhost:58883/");
            DelayForDemoVideo();
        }

        private void DelayForDemoVideo(int delay = 500)
        {
            Thread.Sleep(delay);
        }
    }
}
