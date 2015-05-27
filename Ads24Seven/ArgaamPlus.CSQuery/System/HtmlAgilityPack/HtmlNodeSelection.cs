namespace ArgaamPlus.CSQuery.Systems.HtmlAgilityPack
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::HtmlAgilityPack;

    #endregion

     
    public static class HtmlNodeSelection
    {
        private static readonly HtmlNodeOps _ops = new HtmlNodeOps();

         
        public static HtmlNode QuerySelector(this HtmlNode node, string selector)
        {
            return node.QuerySelectorAll(selector).FirstOrDefault();
        }

         
        public static IEnumerable<HtmlNode> QuerySelectorAll(this HtmlNode node, string selector)
        {
            return QuerySelectorAll(node, selector, null);
        }

         
        public static IEnumerable<HtmlNode> QuerySelectorAll(this HtmlNode node, string selector, Func<string, Func<HtmlNode, IEnumerable<HtmlNode>>> compiler)
        {
            return (compiler ?? CachableCompile)(selector)(node);
        }

         
        public static int CacheSize
        {
            get
            {
                return _compilerCache.Capacity;
            }
            set
            {
                _compilerCache.Capacity = value;
            }
        }

         
        public static Func<HtmlNode, IEnumerable<HtmlNode>> Compile(string selector)
        {
            var compiled = Parser.Parse(selector, new SelectorGenerator<HtmlNode>(_ops)).Selector;
            return node => compiled(Enumerable.Repeat(node, 1));
        }

        //
        // Caching
        //

        private const int DefaultCacheSize = 60;

        private static LRUCache<string, Func<HtmlNode, IEnumerable<HtmlNode>>> _compilerCache = new LRUCache<string, Func<HtmlNode, IEnumerable<HtmlNode>>>(Compile, DefaultCacheSize);
        private static Func<string, Func<HtmlNode, IEnumerable<HtmlNode>>> _defaultCachingCompiler = _compilerCache.GetValue;
            
         
        public static Func<HtmlNode, IEnumerable<HtmlNode>> CachableCompile(string selector)
        {
            return _defaultCachingCompiler(selector);
        }

    }
}
