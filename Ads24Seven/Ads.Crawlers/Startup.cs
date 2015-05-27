using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ads.Crawlers.Startup))]
namespace Ads.Crawlers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
