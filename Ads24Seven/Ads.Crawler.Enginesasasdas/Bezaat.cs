using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ads.CSQuery;
using Ads.CSQuery.Systems.HtmlAgilityPack;

namespace Ads.Crawler.Engines
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Bezaat : BaseEngine
    {
        public List<Category> GetCategories(string url)
        {
            string page = GetPage(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode;
            HtmlNode box = html.QuerySelectorAll("#inner_boxes").FirstOrDefault();
            List<Category> list = new List<Category>();
            foreach (HtmlNode ul in box.QuerySelectorAll("ul").ToList())
            {
                var li = ul.QuerySelectorAll("li").FirstOrDefault();
                if (li != null)
                {
                    var img = li.QuerySelectorAll("img").FirstOrDefault();
                    var link = li.QuerySelectorAll("a").FirstOrDefault();
                    if (link != null)
                    {
                        Category c = new Category();
                        c.URL = link.Attributes["href"].Value;
                        var h = link.QuerySelectorAll("h2").FirstOrDefault();
                        if (h != null)
                        {
                            c.Name = h.InnerText;
                        }
                        if (img != null)
                        {
                            c.Name = img.Attributes["src"].Value;
                        }
                        list.Add(c);
                    }
                }
            }
            return list;
        }
        public class Category
        {
            public string Name { get; set; }
            public string URL { get; set; }
            public string IconURL { get; set; }
            public List<Advertisment> Advertisments { get; set; }
        }
        public class Advertisment
        {
            public string Title { get; set; }
            public string URL { get; set; }
        }
    }

}
