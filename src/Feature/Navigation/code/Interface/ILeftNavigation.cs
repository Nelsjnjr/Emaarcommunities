#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Interface
{
    public interface ILeftNavigation
    {
        #region property
        /// <summary>
        /// Current Context item
        /// </summary>
        INavigable CurrentNavigationItem { get; set; }
        /// <summary>
        /// Parent item of the Context item based on the template(Mediacenter,Generic contentpage and contentpage)
        /// </summary>
        INavigable ParentNavigationItem { get; set; }
        #endregion

    }
}