using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Donation.Myfolder
{
    public class Logger
    {


        public static void WriteLog(string message)
        {
            string logpath = ConfigurationManager.AppSettings["logPath"];

            using (StreamWriter writer = new StreamWriter(logpath, true))
            {
                writer.WriteLine($"{DateTime.Now} : {message}");
            }
        }
    }
}