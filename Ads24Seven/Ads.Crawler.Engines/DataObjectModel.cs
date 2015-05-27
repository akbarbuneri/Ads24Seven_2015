using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Crawler.Engines
{
    [Serializable]
    public class Category
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string IconURL { get; set; }
        public List<Advertisment> Advertisments { get; set; }

        public List<Category> ChildCategories { get; set; }
    }
    [Serializable]
    public class Advertisment
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public List<NameValue> NameValues { get; set; }
        public string URL { get; set; }

        public List<AdImage> Images { get; set; }
    }
    [Serializable]
    public class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    [Serializable]
    public class AdImage
    {
        public string Url { get; set; }
        public bool IsThumb { get; set; }
    }
}
