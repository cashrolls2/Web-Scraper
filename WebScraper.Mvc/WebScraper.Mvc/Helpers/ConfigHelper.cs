using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebScraper.Mvc.Helpers
{
    public static class ConfigHelper
    {
        public static string[] ExcludeWords = ConfigurationManager.AppSettings["ExcludeWords"].Split(',');
        public static int MinimumWordLengthAllowed = int.Parse(ConfigurationManager.AppSettings["MinimumWordLengthAllowed"]);
        public static int TopOccuringWords = int.Parse(ConfigurationManager.AppSettings["TopOccuringWords"]);
        public static int ImagesThreshold = int.Parse(ConfigurationManager.AppSettings["ImagesThreshold"]);
        public static string[] ImageExtensions = ConfigurationManager.AppSettings["ImageExtensions"].Split(',');
        
    }
}