#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Interface
{
    /// <summary>
    /// Getting Sitemap datasource
    /// </summary>
    public interface ISitemapViewModel
    {
        #region property
        // List<KeyValuePair<INavigable, List<INavigable>>> SitemapItems { get; set; }
        /// <summary>
        /// Getting Sitemap details from Header navigation datasource
        /// </summary>
        IHeader Sitemap { get; set; }
        #endregion

    }
}
