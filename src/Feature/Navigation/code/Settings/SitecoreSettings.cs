#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types.Mediacenter;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Settings
{
    public static class SitecoreSettings
    {
        #region Constants
        /// <summary>
        /// Home Template ID
        /// </summary>
        public const string HomeTemplateID  =  IHomeConstants.TemplateIdString;
        /// <summary>
        /// Navigable Template ID
        /// </summary>
        public const string NavigableTemplateID = INavigableConstants.TemplateIdString;
        /// <summary>
        /// Content Page template ID
        /// </summary>
        public const string GenericContentRootPageTemplateID = IGenericContentRootPageConstants.TemplateIdString;
        /// <summary>
        /// Generic ContentPage Template ID
        /// 
        /// </summary>
        public const string GenericContentPageTemplateID  = IGeneric_ContentPageConstants.TemplateIdString;
        /// <summary>
        /// Mediacenter Template ID
        /// </summary>
        public const string MediaCenterTemplateID  = IMediacenterConstants.TemplateIdString;
        /// <summary>
        /// SearchPage Template ID
        /// </summary>
        public const string SearchPageTemplateID = ISearchPageConstants.TemplateIdString;
        
        #endregion
    }
}