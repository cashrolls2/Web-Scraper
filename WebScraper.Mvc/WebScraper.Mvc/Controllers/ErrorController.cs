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
    public class ErrorController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}
