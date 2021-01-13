using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BearTracApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateAppSettingsJSON();
            CreateHostBuilder(args).Build().Run();
        }

        private static void CreateAppSettingsJSON()
        {
            string url = Environment.GetEnvironmentVariable("MONGOURL");
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string path = System.IO.Path.Combine(strWorkPath, "appsettings.json");
            System.Console.WriteLine("Path: " + path);
            string text = "{\"BearTracDatabaseSettings\":{\"ApplicationsCollectionName\": \"Applications\",\"TicketsCollectionName\": \"Tickets\",\"ConnectionString\": \"" + url + "\",\"DatabaseName\": \"BearTracDb\"},\"Logging\": {\"LogLevel\": {\"Default\": \"Information\",\"Microsoft\": \"Warning\",\"Microsoft.Hosting.Lifetime\": \"Information\"}},\"AllowedHosts\": \"*\"}";
            File.WriteAllText(path, text);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
