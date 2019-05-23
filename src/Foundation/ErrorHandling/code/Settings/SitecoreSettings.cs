#region namespace
using static Sitecore.Configuration.Settings;
#endregion
namespace EMAAR.Emaarcommunities.Foundation.ErrorHandling.Settings
{
    public static class SitecoreSettings
    {
        #region Constants
        /// <summary>
        /// LayoutNotFoundUrl
        /// </summary>
        public static string LayoutNotFoundUrl { get; set; } = GetSetting("Emaar.Emaarcommunities.Foundation.ErrorHandling.LayoutNotFoundUrl");
        /// <summary>
        /// ItemNotFoundUrl
        /// </summary>
        public static string ItemNotFoundUrl { get; set; } = GetSetting("Emaar.Emaarcommunities.Foundation.ErrorHandling.ItemNotFoundUrl");


        #endregion
    }
}