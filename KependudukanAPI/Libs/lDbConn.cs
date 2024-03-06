using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace KependudukanAPI.Libs
{
    public class lDbConn
    {
        private lConvert lc = new lConvert();
        /*public IActionResult Index()
        {
            return View();
        }*/

        public string GetStringname(string dbname)
        {
            if (Convert.ToBoolean(ConfigGSM("switcher")) == true)
            {
                return GetAppConfigGSM("DBConstring:" + dbname + "");
            }
            else
            {
                var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

                var config = builder.Build();
                return "" + config.GetSection("DBConstring:" + dbname + "").Value.ToString();
            }
        }

        public string ConfigGSM(string cstr)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            var config = builder.Build();
            return "" + config.GetSection("Secret:" + cstr).Value.ToString();
        }
        public string GetAppConfigGSM(string cstr)
        {
            lGSM lg = new lGSM();
            var config = lg.execExtAPIPost("urlAPI_idcconfig", "GSM/Secret", "master_appsettings");

            return "" + config.GetSection(cstr).Value.ToString();
        }


        public string ConnStringByStringname(string stringname)
        {
            var configDB = "";
            var configPass = "";
            if (Convert.ToBoolean(ConfigGSM("switcher")) == true)
            {
                configPass = lc.decrypt(GetAppConfigGSM("configPass:passwordDB"));
                configDB = GetAppConfigGSM("DbContextSettings:" + stringname + "");
            }
            else
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var config = builder.Build();
                configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
                configDB = config.GetSection("DbContextSettings:" + stringname + "").Value.ToString();
            }

            var repPass = configDB.Replace("{pass}", configPass);
            return "" + repPass;
        }

        public string ConfigKey(string cstr)
        {
            if (Convert.ToBoolean(ConfigGSM("switcher")) == true)
            {
                return GetAppConfigGSM("KeyConvert:" + cstr);
            }
            else
            {
                //var builder = new ConfigurationBuilder()
                //       .SetBasePath(Directory.GetCurrentDirectory())
                //       .AddJsonFile("appsettings.json");

                //var config = builder.Build();
                //return "" + config.GetSection("KeyConvert:" + cstr).Value.ToString();

                return "idxpartners";
            }
        }

        // dari dev
        public string conString()
        {
            var configDB = "";
            var configPass = "";
            if (Convert.ToBoolean(ConfigGSM("switcher")) == true)
            {
                configPass = lc.decrypt(GetAppConfigGSM("configPass:passwordDB"));
                configDB = GetAppConfigGSM("DbContextSettings:ConnectionString_en");
            }
            else
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var config = builder.Build();
//                configPass = lc.decrypt(config.GetSection("configPass:passwordDB").Value.ToString());
                configPass = config.GetSection("configPass:passwordDB").Value.ToString();
                configDB = config.GetSection("DbContextSettings:ConnectionString_en").Value.ToString();
            }

            var repPass = configDB.Replace("{pass}", configPass);
            return "" + repPass;
        }
    }
}
