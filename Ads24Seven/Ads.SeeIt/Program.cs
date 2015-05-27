using Ads.Crawler.Engines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ads.SeeIt
{
    class Program
    {
        static void Main(string[] args)
        {
            Ads.Crawler.Engines.OpenSooq bezaat = new Crawler.Engines.OpenSooq();
            List<Category> list = new List<Category>();
             
            var bc = bezaat.GetCategories("http://ly.opensooq.com/a/%D8%B3%D9%8A%D8%A7%D8%B1%D8%A7%D8%AA-%D9%88-%D9%85%D8%B1%D9%83%D8%A8%D8%A7%D8%AA", "http://ly.opensooq.com");
            foreach (var c in bc)
            {
                Console.WriteLine(c.Name);
            }
            //list.Add(c);
            string xml = "";
            var ser = new XmlSerializer(typeof(List<Category>));
            using (var ms = new MemoryStream())
            {
                ser.Serialize(ms, bc);
                var bytes = ms.ToArray();
                xml = System.Text.ASCIIEncoding.UTF8.GetString(bytes);
            }
            StreamWriter sw = new StreamWriter(@"C:\\OpenSooq.xml");
            sw.Write(xml);
            sw.Close();
            Console.WriteLine("Done ...");
            Console.Read();
        }
        public static StringWriter Serialize(object o)
        {
            var xs = new XmlSerializer(o.GetType());
            var xml = new StringWriter();
            xs.Serialize(xml, o);

            return xml;
        }
    }
}
