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
        private static DriverContext _driverContext;
        private static IConfiguration _config;

        public MainHooks(DriverContext driverContext)
        {
            _driverContext = driverContext;
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
            _driverContext.Driver = new ChromeDriver();
            _driverContext.Wait = new WebDriverWait(_driverContext.Driver, TimeSpan.FromSeconds(10));
            _driverContext.Driver.Navigate().GoToUrl(_config["baseUrl"]);
            Thread.Sleep(5000);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driverContext.Driver.Quit();
        }
    }
}
