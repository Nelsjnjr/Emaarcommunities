#region namespace
using EMAAR.Emaarcommunities.Feature.Navigation.Interface;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Models
{
    /// <summary>
    /// Getting Sitemap datasource
    /// </summary>
    [Service(typeof(ISitemapViewModel))]
    public class SitemapViewModel : ISitemapViewModel
    {
        #region property
        // public List<KeyValuePair<INavigable, List<INavigable>>> SitemapItems { get; set; }
        /// <summary>
        /// Getting Sitemap details from Header navigation datasource
        /// </summary>
        public IHeader Sitemap { get; set; }
        #endregion
    }

}