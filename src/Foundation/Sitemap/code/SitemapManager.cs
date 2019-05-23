using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Security.Accounts;
using Sitecore.Sites;
using Sitecore.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Xml;
using Sitecore.Modules.SitemapXML;
using Sitecore.Globalization;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore.Xml;

namespace EMAAR.Emaarcommunities.Foundation.Sitemap
{
    /// <summary>
    /// This code was ported from Sitecore's Sitemap XML module.  It was updated to support retrieving URL by the languages supported by the site
    /// </summary>
    public class SitemapManager
    {
        private static System.Collections.Specialized.StringDictionary m_Sites;

        public Database Db
        {
            get
            {
                return Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
            }
        }      
        public SitemapManager()
        {
            
            foreach (var mSite in GetSitemapSitesDetails())
            {
                this.BuildSiteMap(mSite.Sitename, mSite.FileName, mSite.IsSeparatelanguagefile);
            }
        }
        private List<(string Sitename, string FileName, string IsSeparatelanguagefile)> GetSitemapSitesDetails()
        {
            List<(string Sitename, string FileName, string IsSeparatelanguagefile)> sitedetails = new List<(string Sitename, string FileName, string IsSeparatelanguagefile)>();
            foreach (XmlNode configNode in Factory.GetConfigNodes("sitemapVariables/sites/site"))
            {
                if (!string.IsNullOrEmpty(XmlUtil.GetAttribute("name", configNode)) && 
                    !string.IsNullOrEmpty(XmlUtil.GetAttribute("filename", configNode)) &&
                    !string.IsNullOrEmpty(XmlUtil.GetAttribute("isseparatelanguagefile", configNode)))
                    sitedetails.Add(
                        (Sitename: XmlUtil.GetAttribute("name", configNode),
                        FileName: XmlUtil.GetAttribute("filename", configNode),
                        IsSeparatelanguagefile: XmlUtil.GetAttribute("isseparatelanguagefile", configNode)));
            }        
            return sitedetails;
        }

        private List<string> BuildListFromString(string str, char separator)
        {
            if (String.IsNullOrEmpty(str))
                str = String.Empty;

            char[] chrArray = new char[] { separator };
            IEnumerable<string> strs =
                from dtp in str.Split(chrArray)
                where !string.IsNullOrEmpty(dtp)
                select dtp;
            return strs.ToList<string>();
        }

        private void BuildSiteMap(string Sitename, string FileName, string IsSeparatelanguagefile)
        {

            List<Item> sitemapItems;
            Site site = SiteManager.GetSite(Sitename);
            SiteContext siteContext = Factory.GetSite(Sitename);

            XmlDocument xDoc = null;
            List<(XmlDocument doc, string lang)> sitedetails = new List<(XmlDocument doc, string lang)>();
            List<string> enabledLanguages = BuildListFromString(siteContext.Properties["enabledLanguages"], '|');
            if (enabledLanguages.Count == 0)
            {   //if the config doesn't specify any enabled languages, use the current context to retrieve items
                sitemapItems = this.GetSitemapItems(siteContext.StartPath, null);
                xDoc = StartSitemapXML();
                xDoc = this.BuildSitemapXML(xDoc, sitemapItems, site, null);
                sitedetails.Add((xDoc, ""));
            }
            else
            {
                //For each language supported by the site, retrieve the items using the language and build out sitemap links
                foreach (var language in enabledLanguages)
                {
                    sitemapItems = this.GetSitemapItems(siteContext.StartPath, Sitecore.Globalization.Language.Parse(language));
                    xDoc = StartSitemapXML();
                    xDoc = this.BuildSitemapXML(xDoc, sitemapItems, site, Sitecore.Globalization.Language.Parse(language));
                    if(IsSeparatelanguagefile.ToLower()=="yes")
                        sitedetails.Add((xDoc, language));
                    else
                        sitedetails.Add((xDoc, ""));
                }
            }
            foreach (var details in sitedetails)
            {
                string str = String.Empty;
                string filename = !String.IsNullOrWhiteSpace(details.lang) ? String.Format("{0}-{1}.xml", FileName, details.lang) : FileName;
                str= MainUtil.MapPath(string.Concat("/",filename));
                StreamWriter streamWriter = new StreamWriter(str, false);
                streamWriter.Write(details.doc.OuterXml);
                streamWriter.Close();
            }
          
        }
        
        private XmlDocument BuildSitemapItem(XmlDocument doc, Item item, Site site, Language language)
        {
            var str = HttpUtility.UrlPathEncode(this.GetItemUrl(item, site, language));
            DateTime updated = item.Statistics.Updated;
            string str1 = SitemapManager.HtmlEncode(updated.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            string priority = GetSitemapPriority(item);
            XmlNode lastChild = doc.LastChild;
            XmlNode xmlNodes = doc.CreateElement("url");
            lastChild.AppendChild(xmlNodes);
            XmlNode xmlNodes1 = doc.CreateElement("loc");
            xmlNodes.AppendChild(xmlNodes1);
            xmlNodes1.AppendChild(doc.CreateTextNode(str));
            XmlNode xmlNodes2 = doc.CreateElement("lastmod");
            xmlNodes.AppendChild(xmlNodes2);
            xmlNodes2.AppendChild(doc.CreateTextNode(str1));
            XmlNode xmlNodesChangeFreq = doc.CreateElement("changefreq");
            xmlNodes.AppendChild(xmlNodesChangeFreq);
            xmlNodesChangeFreq.AppendChild(doc.CreateTextNode("daily"));
            XmlNode xmlNodesPriority = doc.CreateElement("priority");
            xmlNodes.AppendChild(xmlNodesPriority);
            xmlNodesPriority.AppendChild(doc.CreateTextNode(priority));
            return doc;
        }

        /// <summary>
        /// Returns sitemap priority value based on template
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetSitemapPriority(Item item)
        {
            var templateId = item.TemplateID.ToString();
            switch (templateId)
            {
                case (CommonConstants.HomePageTemplateID):
                case (CommonConstants.GenericContentPageTemplateID):
                    return "1.0";
                case (CommonConstants.MediaCenterPageTemplateID):
                    return "0.9";
                case (CommonConstants.NewsTemplateID):
                case (CommonConstants.EventsTemplateID):
                case (CommonConstants.DownloadsTemplateID):
                case (CommonConstants.ImageGalleryPageTemplateID):
                case (CommonConstants.ImageAlbumPageTemplateID):
                case (CommonConstants.VideoGalleryTemplateID):
                case (CommonConstants.VideoAlbumWithFiltersTemplateID):
                case (CommonConstants.VideoAlbumWithoutFiltersTemplateID):
                    return "0.8";
                default:
                    return "0.7";
            }
        }

        private XmlDocument StartSitemapXML()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNodes = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.AppendChild(xmlNodes);
            XmlNode xmlNodes1 = xmlDocument.CreateElement("urlset");
            XmlAttribute xmlnsTpl = xmlDocument.CreateAttribute("xmlns");
            xmlnsTpl.Value = SitemapManagerConfiguration.XmlnsTpl;
            xmlNodes1.Attributes.Append(xmlnsTpl);
            xmlDocument.AppendChild(xmlNodes1);

            return xmlDocument;
        }
        private XmlDocument BuildSitemapXML(XmlDocument xmlDocument, List<Item> items, Site site, Language language)
        {
            foreach (Item item in items)
            {
                xmlDocument = this.BuildSitemapItem(xmlDocument, item, site, language);
            }

            return xmlDocument;
        }

        public List<Item> GetHeaders(List<Item> items, Site site, Language language)
        {
            List<Item> listItems = new List<Item>();
            try
            {
                System.Threading.Tasks.Parallel.ForEach(items, item =>
                {
     

                    HttpWebRequest request = WebRequest.Create(HttpUtility.UrlPathEncode(this.GetItemUrl(item, site, language))) as HttpWebRequest;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
                    request.Method = "HEAD";
                    request.Timeout = 60000;
                      
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null)
                        {
                            if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300 && request.RequestUri.AbsoluteUri == response.ResponseUri.AbsoluteUri)
                                listItems.Add(item);
                            response.Close();
                            response.Dispose();
                        }
                    }
                });

                return listItems;
            }
            catch(Exception error)
            {
                Sitecore.Diagnostics.Log.Error("Error while checking response statuscode for Sitemap", error.Message);
                return listItems;
            }
        }

        private string GetItemUrl(Item item, Site site, Language language)
        {
            UrlOptions defaultOptions = UrlOptions.DefaultOptions;
            defaultOptions.SiteResolving = Settings.Rendering.SiteResolving;
            defaultOptions.Site = SiteContext.GetSite(site.Name);
            defaultOptions.AlwaysIncludeServerUrl = false;
            defaultOptions.LanguageEmbedding = LanguageEmbedding.Always;
            if (language != null)
                defaultOptions.Language = language;
            string itemUrl = LinkManager.GetItemUrl(item, defaultOptions);
            string serverUrlBySite = SitemapManagerConfiguration.GetServerUrlBySite(site.Name);
            if (serverUrlBySite.Contains("http://"))
            {
                serverUrlBySite = serverUrlBySite.Substring("http://".Length);
            }
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(serverUrlBySite))
            {
                if (!(itemUrl.Contains("://") || itemUrl.Contains("http")))
                {
                    stringBuilder.Append("http://");
                    stringBuilder.Append(serverUrlBySite);
                    stringBuilder.Append(itemUrl);
                }
                else
                {
                    if (itemUrl.StartsWith("http"))
                        itemUrl = itemUrl.Remove(0, 4);
                    stringBuilder.Append("http://");
                    stringBuilder.Append(serverUrlBySite);
                    if (itemUrl.IndexOf("/", 3) > 0)
                    {
                        stringBuilder.Append(itemUrl.Substring(itemUrl.IndexOf("/", 3)));
                    }
                }
            }
            else if (!string.IsNullOrEmpty(site.Properties["hostname"]))
            {
                stringBuilder.Append("http://");
                stringBuilder.Append(site.Properties["hostname"]);
                stringBuilder.Append(itemUrl);
            }
            else if (!itemUrl.Contains("://") || itemUrl.Contains("http"))
            {
                stringBuilder.Append(WebUtil.GetFullUrl(itemUrl));
            }
            else
            {
                stringBuilder.Append("http://");
                stringBuilder.Append(itemUrl);
            }
            return stringBuilder.ToString();
        }

        private List<Item> GetSitemapItems(string rootPath, Language language)
        {
            Item[] descendants;
            string enabledTemplates = SitemapManagerConfiguration.EnabledTemplates;
            string excludeItems = SitemapManagerConfiguration.ExcludeItems;
            Database database = Factory.GetDatabase(SitemapManagerConfiguration.WorkingDatabase);
            Item item;
            if (language == null)
                item = database.Items.GetItem(rootPath);
            else
                item = database.Items.GetItem(rootPath, language);
            if (item != null)
            {
                using (UserSwitcher userSwitcher = new UserSwitcher(User.FromName("extranet\\Anonymous", true)))
                {
                    descendants = item.Axes.GetDescendants();
                }
                List<Item> list = descendants.ToList<Item>();
                list.Insert(0, item);
                List<string> strs = this.BuildListFromString(enabledTemplates, '|');
                List<string> strs1 = this.BuildListFromString(excludeItems, '|');
                IEnumerable<Item> items = list.Where<Item>((Item itm) =>
                {
                    if (itm.Template == null || !strs.Contains(itm.TemplateID.ToString()))
                    {
                        return false;
                    }
                    return !strs1.Contains(itm.ID.ToString());
                });
                return items.Where(x => x != null && x.Versions.Count > 0  && x.Versions.IsLatestVersion() &&
                (CheckboxField)x.Fields[CommonConstants.IncludeinSitemap] != null && ((CheckboxField)x.Fields[CommonConstants.IncludeinSitemap]).Checked).ToList<Item>();
            }
            else
                return new List<Item>();
        }

        private static string HtmlEncode(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

  

        private void SubmitEngine(string engine, string sitemapUrl)
        {
            if (!sitemapUrl.Contains("http://localhost"))
            {
                string str = string.Concat(engine, SitemapManager.HtmlEncode(sitemapUrl));
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(str);
                try
                {
                    if (((HttpWebResponse)httpWebRequest.GetResponse()).StatusCode != HttpStatusCode.OK)
                    {
                        Log.Error(string.Format("Cannot submit sitemap to \"{0}\"", engine), this);
                    }
                }
                catch
                {
                    Log.Warn(string.Format("The serachengine \"{0}\" returns an 404 error", str), this);
                }
            }
        }

        public bool SubmitSitemapToSearchenginesByHttp()
        {
            if (!SitemapManagerConfiguration.IsProductionEnvironment)
            {
                return false;
            }
            bool flag = false;
            Item item = this.Db.Items.GetItem(SitemapManagerConfiguration.SitemapConfigurationItemPath);
            if (item != null)
            {
                string value = item.Fields["Search engines"].Value;
                string[] strArrays = value.Split(new char[] { '|' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    Item item1 = this.Db.Items.GetItem(str);
                    if (item1 != null)
                    {
                        string value1 = item1.Fields["HttpRequestString"].Value;
                        foreach (string str1 in SitemapManager.m_Sites.Values)
                        {
                            this.SubmitEngine(value1, str1);
                        }
                    }
                }
                flag = true;
            }
            return flag;
        }
    }
}
