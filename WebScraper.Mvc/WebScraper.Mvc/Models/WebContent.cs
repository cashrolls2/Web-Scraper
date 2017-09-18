using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScraper.Mvc.Models
{
    public class WebContent
    {
        public string Url { get; set; }
        public string RawText { get; set; }
        public IList<string> Images { get; set; }
        public IEnumerable<KeyValuePair<string, int>> TopWords { get; set; }
        public string[] AllWords { get; set; }
    }
}