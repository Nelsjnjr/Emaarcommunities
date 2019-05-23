#region namespace
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types;
#endregion
namespace EMAAR.Emaarcommunities.Feature.ContentComponents.Settings
{
    public static class SitecoreSettings
    {
        #region property
        /// <summary>
        /// SiteRoot Template ID
        /// </summary>
        public static string SiteRootTemplateID { get; set; } = ISiteRootConstants.TemplateIdString;
        /// <summary>
        /// Generic Template ID
        /// </summary>
        public static string GenericTemplateID { get; set; } = IGeneric_ContentPageConstants.TemplateIdString;
        /// <summary>
        /// CSS Field Name from Siteroot template
        /// </summary>
        public static string SiteRootCSSFieldName { get; set; } = ISiteRootConstants.CSSFileFieldName;
        /// <summary>
        /// News Page Template ID
        /// </summary>
        public static string NewsPageTemplateID { get; set; } = INews_PageConstants.TemplateIdString;
        /// <summary>
        /// Events Page Template ID
        /// </summary>
        public static string EventsPageTemplateID { get; set; } = IEvent_PageConstants.TemplateIdString;
        #endregion
    }
}