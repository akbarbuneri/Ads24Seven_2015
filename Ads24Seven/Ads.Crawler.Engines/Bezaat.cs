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
          //  HtmlNode box = html.QuerySelectorAll("#inner_boxes").FirstOrDefault();
            List<Category> list = new List<Category>();
            foreach (var box in html.QuerySelectorAll("section").ToList())
            {
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
                                c.IconURL = img.Attributes["src"].Value;
                            }
                            try
                            {
                                c.ChildCategories = GetChildCategories(c);
                                c.Advertisments = GetAdvertisements(c.URL);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            list.Add(c);
                        }
                    }
                }
                break;
            }
            return list;
        }

        public List<Category> GetChildCategories(Category parent)
        {
            string page = GetPage(parent.URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode;
            //  HtmlNode box = html.QuerySelectorAll("#inner_boxes").FirstOrDefault();
            List<Category> list = new List<Category>();
            var sid_cat = html.QuerySelectorAll(".sid-cat").FirstOrDefault();
            foreach (var a in sid_cat.QuerySelectorAll("a").ToList())
            {
                Category cat = new Category();
                cat.URL = a.Attributes["href"].Value;
                cat.Name = a.InnerText.Trim().Replace("»", "") ;
                try
                {
                    cat.Advertisments = GetAdvertisements(cat.URL);
                }
                catch (Exception) { }
                list.Add(cat);
            }

            return list;
        }
        public List<Advertisment> GetAdvertisements(string url)
        {
            List<Advertisment> ads = new List<Advertisment>();
            string page = GetPage(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode;
            foreach (var advItem in html.QuerySelectorAll(".adv_item").ToList())
            {
                var adv_content = advItem.QuerySelectorAll(".adv_content").FirstOrDefault();
                if (adv_content != null)
                {
                    var link = adv_content.QuerySelectorAll("a").FirstOrDefault();
                    var h2 = link.QuerySelectorAll("h2").FirstOrDefault();
                    if (link != null)
                    {
                        Advertisment ad = new Advertisment();
                        ad.URL = link.Attributes["href"].Value;
                        ad.Title = h2.InnerText;
                        ad.Images = new List<AdImage>();
                        ad.NameValues = new List<NameValue>();
                        try
                        {
                            FillAd(ad);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        ads.Add(ad);
                    }
                }
            }
            return ads;
        }

        public void FillAd(Advertisment ad)
        {
            string page = GetPage(ad.URL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode html = doc.DocumentNode;
            var title = html.QuerySelectorAll(".title-top").FirstOrDefault().QuerySelectorAll("h1").FirstOrDefault();
            if (title != null)
            {
                ad.Title = title.InnerText.Trim();
            }
            var image_gallery = html.QuerySelectorAll("#image-gallery").FirstOrDefault();
            if (image_gallery != null)
            {
                foreach (var img in image_gallery.QuerySelectorAll("img").ToList())
                {
                    if (img != null)
                    {
                        if (img.Attributes["width"] == null)
                        {
                            ad.Images.Add(new AdImage() { Url = img.Attributes["src"].Value, IsThumb = true });
                        }
                        else
                        {
                            ad.Images.Add(new AdImage() { Url = img.Attributes["src"].Value, IsThumb = false });

                        }
                    }
                }
                var des_bot_in = html.QuerySelectorAll(".des-bot").FirstOrDefault();
                if (des_bot_in != null)
                {
                    var p = des_bot_in.QuerySelectorAll("p").FirstOrDefault();
                    if (p != null)
                    {
                        ad.Description = p.InnerText;
                    }
                }
                var ul = image_gallery.QuerySelectorAll("ul").FirstOrDefault();
                if (ul != null)
                {
                    foreach (var il in ul.QuerySelectorAll("li").ToList())
                    {
                        var span = il.QuerySelectorAll("span").LastOrDefault();
                        if (span != null)
                        {
                            string v = span.InnerText.Trim();
                            string name = il.InnerText.Trim().Replace("»", "");
                            if(!string.IsNullOrEmpty(v))
                            {
                                name = name.Replace(v, "");
                            }
                            ad.NameValues.Add(new NameValue() {Name = name, Value = v });
                        }
                    }
                }
            }

        }
       
    }

}
