using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMAAR.Emaarcommunities.Foundation.Sitemap
{
    public class SitemapHandler
    {
        public SitemapHandler()
        {
        }

        public void RefreshSitemap(object sender, EventArgs args)
        {
            SitemapManager sitemapManager = new SitemapManager();
            sitemapManager.SubmitSitemapToSearchenginesByHttp();          
        }
    }
}
