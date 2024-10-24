using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Globalization;

namespace poc_serilog_dateformat
{
    internal class Program
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();


            // DateTime format for log entries lines is set via the appsettings.json file
            Log.Information("Hello, Serilog!");


            // Custom DateTime format for log entries

            var dateTime = DateTime.Now.ToString(DateTimeFormat);
            Log.Information("DateTime custom formatted: " + dateTime);


               

            // Setting the global DataTime format
            
            CultureInfo customCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            customCulture.DateTimeFormat.LongTimePattern = "HH:mm:ss.fff";
            CultureInfo.DefaultThreadCurrentCulture = customCulture;
            CultureInfo.DefaultThreadCurrentUICulture = customCulture;

                             
            Log.Information("DateTime global culture  : " + DateTime.Now.ToString());


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
