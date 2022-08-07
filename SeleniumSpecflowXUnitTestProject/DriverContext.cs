using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSpecflowXUnitTestProject
{
    public class DriverContext
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        public string CaptureScreenshotAndReturnAsBase64()
        {
            return ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;
        }
    }
}
