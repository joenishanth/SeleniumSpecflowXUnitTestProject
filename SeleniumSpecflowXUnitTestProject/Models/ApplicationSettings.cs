using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSpecflowXUnitTestProject.Models
{
    public class ApplicationSettings
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DataBase DataBase { get; set; }
    }

    public class DataBase
    {
        public string ConnectionString { get; set; }
        public string AccessToken { get; set; }
    }
}
