using Microsoft.Data.SqlClient;
using SeleniumSpecflowXUnitTestProject.Hooks;
using SeleniumSpecflowXUnitTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSpecflowXUnitTestProject.Configuration
{
    public class DatabaseConfiguration
    {
        private ApplicationSettings _config;

        public DatabaseConfiguration()
        {
            _config = MainHooks.Configuration;
        }
       public void  DBConnection()
        {
            var newDbConnection = new SqlConnection(_config.DataBase.ConnectionString);
            newDbConnection.AccessToken = _config.DataBase.AccessToken;
        }
    }
}
