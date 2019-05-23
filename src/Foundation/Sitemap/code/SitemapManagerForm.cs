using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;
using Sitecore.Modules.SitemapXML;

namespace EMAAR.Emaarcommunities.Foundation.Sitemap
{
    public class SitemapManagerForm : BaseForm
    {
        protected Button RefreshButton;

        protected Literal Message;

        public SitemapManagerForm()
        {

        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
            if (!Context.ClientPage.IsEvent)
            {
                this.RefreshButton.Click = "RefreshButtonClick";
            }
        }

        protected void RefreshButtonClick()
        {
            (new SitemapHandler()).RefreshSitemap(this, new EventArgs());
            StringDictionary sites = SitemapManagerConfiguration.GetSites();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string value in sites.Values)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append(String.Format("{0}.xml",value));
            }
            string str = string.Format(" - The sitemap file <b>\"{0}\"</b> has been refreshed<br /> ",stringBuilder.ToString());
            this.Message.Text = str;
            SitemapManagerForm.RefreshPanel("MainPanel");
        }

        private static void RefreshPanel(string panelName)
        {
            Panel panel = Context.ClientPage.FindControl(panelName) as Panel;
            Assert.IsNotNull(panel, "can't find panel");
            Context.ClientPage.ClientResponse.Refresh(panel);
        }
    }
}
