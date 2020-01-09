using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Xunit;

namespace Cookbook.HighLevelTests
{
    public class HighLevelTest : IDisposable
    {
        public readonly IWebDriver _driver;

        public HighLevelTest()
        {
            _driver = new ChromeDriver(Environment.CurrentDirectory);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void DelayForDemoVideo(int delay = 1)
        {
            Thread.Sleep(500 * delay);
        }
    }
}
