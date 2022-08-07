using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumSpecflowXUnitTestProject.Hooks;
using SeleniumSpecflowXUnitTestProject.Models;
using SeleniumSpecflowXUnitTestProject.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumSpecflowXUnitTestProject.Steps
{
    [Binding]
    public class LoginGuru99AppSteps
    {
        private readonly DriverContext _driverContext;
        private readonly ApplicationSettings _config;
        private readonly LoginPage _loginPage;
        public LoginGuru99AppSteps(ScenarioContext scenarioContext)
        {
            _driverContext = scenarioContext.Get<DriverContext>();

            _loginPage = new LoginPage(_driverContext);
            _config = MainHooks.Configuration;
        }

            [Given(@"I am in the application Login page")]
        public void GivenIAmInTheApplicationLoginPage()
        {
             
            var baseUrl = _config.BaseUrl;
            _driverContext.Driver.Navigate().GoToUrl(baseUrl);
        }

        [When(@"I enter a valid login credentials")]
        public void WhenIEnterAValidLoginCredentials()
        {
            _loginPage.SetUserName(_config.UserName);
            _loginPage.SetPassword(_config.Password);
            _loginPage.SelectLogin();
        }

        [Then(@"I should be in the application home page")]
        public void ThenIShouldBeInTheApplicationHomePage()
        {
            var exppHomepageTitle = "Guru99 Bank Manager HomePage";
            var actHomepageTitle = _driverContext.Driver.Title;

            Assert.Equal(exppHomepageTitle, actHomepageTitle);
        }

        [When(@"I enter a invalid login credentials")]
        public void WhenIEnterAInvalidLoginCredentials()
        {
            _loginPage.SetUserName("Abcd");
            _loginPage.SetPassword("Abcd1234");
            _loginPage.SelectLogin();
        }

        [Then(@"I should invalid login alert")]
        public void ThenIShouldInvalidLoginAlert()
        {
            var exppAlertMsg = "User or Password is not valid"; 
            _driverContext.Wait.Until(ExpectedConditions.AlertIsPresent());
            var actualalertMsg = _driverContext.Driver.SwitchTo().Alert().Text;

            Assert.Equal(exppAlertMsg, actualalertMsg);
        }

    }
}
