using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KependudukanAPI.Controllers
{
    public class MessageController
    {
        public string GetMessage(string code)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("message.json");

            var config = builder.Build();
            return config.GetSection(code).Value.ToString();
        }
    }
}
