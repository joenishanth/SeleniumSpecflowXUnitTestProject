using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace SeleniumSpecflowXUnitTestProject.Hooks
{
    [Binding]
    public sealed class MainHooks
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private static IConfiguration _config;

        public MainHooks(DriverContext driverContext)
        {
            _driver = driverContext.Driver;
            _wait = driverContext.Wait;
        }

        [BeforeTestRun]
        public static void BeforeAllTest()
        {
             _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appSettings.json")
                .Build();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(_config["baseUrl"]);
            Thread.Sleep(5000);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }
    }
}
