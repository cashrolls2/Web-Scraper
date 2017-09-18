using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebScraper.Mvc.Helpers;
using WebScraper.Mvc.Models;
using WebScraper.Mvc.Scraper;

namespace WebScraper.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                return View("error", new Error { Message = "An invalid URL was entered" });
            }

            //Data model
            var content = new WebContent();

            try
            {
                //scraper class instance
                var scraper = new HtmlScraper();

                //Scrape data from website, get content and image urls
                content = scraper.GetWebContent(url);

                //populate content object words property with grouped words and count
                WordsHelper.GroupWordsByOccurrences(content, ConfigHelper.ExcludeWords, ConfigHelper.MinimumWordLengthAllowed, ConfigHelper.TopOccuringWords);
            }

            catch (Exception ex)
            {
                return Redirect("/error/404.html");
            }

            return View("index", content); //return "Events" view to the user
        }
    }
}
