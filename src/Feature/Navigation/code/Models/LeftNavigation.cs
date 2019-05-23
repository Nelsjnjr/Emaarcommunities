#region namespace
using EMAAR.Emaarcommunities.Feature.Navigation.Interface;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Models
{
    /// <summary>
    /// This class used to load Left Navigation component
    /// </summary>
    [Service(typeof(ILeftNavigation))]
    public class LeftNavigation : ILeftNavigation
    {
        #region property
        /// <summary>
        /// Current Context item
        /// </summary>
        public INavigable CurrentNavigationItem { get ; set ; }
        /// <summary>
        /// Parent item of the Context item based on the template(Mediacenter,Generic contentpage and contentpage)
        /// </summary>
        public INavigable ParentNavigationItem { get; set; }
        #endregion

    }
}