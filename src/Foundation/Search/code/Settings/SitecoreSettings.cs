using static Sitecore.Configuration.Settings;

namespace EMAAR.Emaarcommunities.Foundation.Search.Settings
{
    public static class SitecoreSettings
    {
        /// <summary>
        /// PlaceholdersToSearch
        /// </summary>
        public static string PlaceholdersToSearch { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.Search.PlaceholdersToSearch");

        /// <summary>
        /// BasePageTemplateId
        /// </summary>
        public static string BasePageTemplateId { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.Search.BasePageTemplateId");
        
        /// <summary>
        /// ExcludeTemplatesFromSearchPage
        /// </summary>
        public static string ExcludeTemplatesFromSearchPage { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.Search.ExcludeTemplatesFromSearchPage");

        /// <summary>
        /// ExcludeItemNamesFromSearchPage
        /// </summary>
        public static string ExcludeItemNamesFromSearchPage { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.Search.ExcludeItemNamesFromSearchPage");
    }
}