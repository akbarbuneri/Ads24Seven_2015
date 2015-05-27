using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ads.Crawler.Engines
{
    public class BaseEngine
    {
        public string GetPage(string url)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.Headers[HttpRequestHeader.Accept] = "text/html, image/png, image/jpeg, image/gif, */*;q=0.1";
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; de; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12";
            string str = client.DownloadString(url);
            return str;
        }
    }
}
