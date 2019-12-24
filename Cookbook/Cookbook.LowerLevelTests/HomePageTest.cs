using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Cookbook.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.LowerLevelTests
{
    public class HomePageTest
    {
        [Fact]
        public void TestIndex()
        {
            var homeController = new HomeController();
            var result = homeController.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
