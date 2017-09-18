using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebScraper.Mvc.Helpers;
using WebScraper.Mvc.Models;

namespace WebScraper.Mvc.Scraper
{
    public class HtmlScraper
    {
        public WebContent GetWebContent(string url)
        {
            try
            {
                List<string> images = new List<string>();

                HtmlWeb client = new HtmlWeb();

                //Load HTML document for specified URL using Nuget HtmlAgilityPack library
                HtmlDocument doc = client.Load(url);

                //Select all src UrLs from image nodes
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//img[@src]");

                //Loop through nodes and store image urls to list
                foreach (var link in nodes)
                {
                    //Verify image extension is one that is noted in web.config
                    if (ConfigHelper.ImageExtensions.Any(link.Attributes["src"].Value.Contains))
                    {   
                        images.Add(link.Attributes["src"].Value);
                    }
                }

                //Remove script ad style  tags
                foreach (var script in doc.DocumentNode.Descendants("script").ToArray())
                {
                    script.Remove();
                }

                foreach (var style in doc.DocumentNode.Descendants("style").ToArray())
                {
                    style.Remove();
                }

                //Return model
                return new WebContent
                {
                    RawText = doc.DocumentNode.InnerText,
                    Images = images
                };
            }
            catch (HttpException hex)
            {
                throw new HttpException(hex.Message);
            }
            catch (Exception ex)
            {
                throw new HttpException(ex.Message);
            }

        }
    }
}