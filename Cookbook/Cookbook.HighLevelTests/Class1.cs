using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Xunit;

namespace Cookbook.HighLevelTests
{
    public class Class1 : IDisposable
    {
        private readonly IWebDriver _driver;

        public Class1()
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
            DelayForDemoVideo(1000);
            
            Assert.Contains("Hello World!", _driver.PageSource.ToString());
        }

        private void DelayForDemoVideo(int delay = 500)
        {
            Thread.Sleep(delay);
        }
    }
}
