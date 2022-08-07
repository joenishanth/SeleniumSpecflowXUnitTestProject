using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSpecflowXUnitTestProject.Configuration
{
    public  class NetworkInterception
    {
        private static DriverContext _driverContext;
        public NetworkInterception(DriverContext driverContext)
        {
            _driverContext = driverContext;
            INetwork abcd = _driverContext.Driver.Manage().Network;
            //var respHandler = new NetworkResponseHandler()
            //{
            //    ResponseMatcher = 
            //};
            // abcd.AddResponseHandler(respHandler);
        }


    }
}
