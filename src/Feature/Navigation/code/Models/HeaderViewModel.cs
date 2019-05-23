#region namespace
using EMAAR.Emaarcommunities.Feature.Navigation.Interface;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Models
{
    /// <summary>
    /// This class used to load header component
    /// </summary>
    [Service(typeof(IHeaderViewModel))]
    public class HeaderViewModel : IHeaderViewModel
    {
        #region property
        /// <summary>
        /// Getting Header Menu details from Sitecore
        /// </summary>
        public IHeader Header { get; set; }

        public ISiteRoot SiteRoot  { get; set; }
        /// <summary>
        /// Getting Logo details from Sitecore SiteRoot item
        /// </summary>
        public ILogo Logos { get; set; }
        /// <summary>
        /// Getting Search Icon
        /// </summary>
        public string SearchIcon { get; set; }
        /// <summary>
        /// Getting Close icon
        /// </summary>
        public string CloseIcon { get; set; }
        /// <summary>
        /// Getting Header CSS Class name
        /// </summary>
        public string HeaderCss { get; set; }
        /// <summary>
        /// Check is this page is Home
        /// </summary>
        public bool IsHomePage { get; set; }
        /// <summary>
        /// Check is this Search Page
        /// </summary>
        public bool IsSearchPage { get; set; }
        #endregion
    }
}