using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.CSQuery;
using Ads.CSQuery.Systems.HtmlAgilityPack;

namespace Ads.Crawler.Engines
{
    public class OpenSooq : BaseEngine
    {
        public List<Category> GetCategories(string url, string rootUrl)
        {
            string page = GetPage(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode; 
            List<Category> list = new List<Category>();
            var lvl_1_container = html.QuerySelectorAll(".subRightList").LastOrDefault();
            foreach (var a in lvl_1_container.QuerySelectorAll("a").ToList().Skip(1))
            {
                Category cat = new Category();
                cat.Name = a.InnerText;
                cat.URL =rootUrl +"/"+ a.Attributes["href"].Value;
                cat.ChildCategories = new List<Category>();
                cat.ChildCategories = GetChildCategories(cat, rootUrl);
                list.Add(cat);
            }
            return list;
        }

        public List<Category> GetChildCategories(Category parent, string rootUrl)
        {
            string page = GetPage(parent.URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode; 
            List<Category> list = new List<Category>();
            var subLeftList = html.QuerySelectorAll(".subLeftList").FirstOrDefault();
            if (subLeftList != null)
            {
                foreach (var a in subLeftList.QuerySelectorAll("a").ToList())
                {
                    var span = a.QuerySelectorAll("span").LastOrDefault();
                    if (span != null)
                    {
                        Category cat = new Category();
                        cat.URL = rootUrl + "/" + a.Attributes["href"].Value;
                        cat.Name = span.InnerText.Trim();
                        cat.Advertisments = GetAdvertisements(cat.URL, rootUrl);
                        list.Add(cat);
                    }
                }
            }
            return list;
        }
        public List<Advertisment> GetAdvertisements(string url, string rootUrl)
        {
            List<Advertisment> ads = new List<Advertisment>();
            string page = GetPage(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode;
            foreach (var advItem in html.QuerySelectorAll(".rectLiDetails").ToList())
            {
                var h3 = advItem.QuerySelectorAll("h3").FirstOrDefault();
                if (h3 != null)
                {
                    var a = h3.QuerySelectorAll("a").FirstOrDefault();
                    if (a != null)
                    {
                        Advertisment ad = new Advertisment();
                        ad.Title = a.InnerText.Trim();
                        ad.URL = rootUrl +"/"+ a.Attributes["href"].Value;
                        ads.Add(ad);
                    }
                }
            }
            return ads;
        }

    }
}
