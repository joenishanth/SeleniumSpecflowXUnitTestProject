using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumSpecflowXUnitTestProject.PageObjects
{
    public  class LoginPage
    {
        private readonly DriverContext _driverContext;
        public LoginPage(DriverContext driverContext)
        {
            _driverContext = driverContext;
        }

        public IWebElement UserNameCSS => _driverContext.Driver.FindElement(By.Name("uid"));
        public IWebElement PasswordCSS => _driverContext.Driver.FindElement(By.Name("password"));
        public IWebElement LoginBtnCSS => _driverContext.Driver.FindElement(By.Name("btnLogin"));

        public void GotoApp(string baseUrl) => _driverContext.Driver.Navigate().GoToUrl(baseUrl);
        public void SetUserName(string userName) => UserNameCSS.SendKeys(userName);
        public void SetPassword(string password) => PasswordCSS.SendKeys(password); 
        public void SelectLogin() => LoginBtnCSS.Click();


    }
}
