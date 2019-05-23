#region namespace
using static Sitecore.Configuration.Settings;
#endregion
namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings
{
    /// <summary>
    /// This class provides the common setting for Banner projects
    /// </summary>
    public static class SitecoreSettings
    {
        /// <summary>
        ///  Getting Root View location for controller to load view
        ///  Physical location of the view path for the action method
        ///  ~/Views/Feature/Emaarcommunities/
        /// </summary>
        public static string ViewPath { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.FeatureViewPath");
        /// <summary>
        ///  Getting Home page search icon image url from Sitecore settings based on Site
        ///  /Site Settings/Site Specific Images/Home/HomePageSearch
        /// </summary>
        public static string HomePageSearch { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.HomePageSearch");
        /// <summary>
        ///  Getting Home page Close icon image url from Sitecore settings based on Site
        ///  /Site Settings/Site Specific Images/Home/HomePageClose
        /// </summary>
        public static string HomePageClose { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.HomePageClose");
        /// <summary>
        ///  Getting Content page search icon image url from Sitecore settings based on Site
        ///  /Site Settings/Site Specific Images/Content/ContentPageSearch
        /// </summary>
        public static string ContentPageSearch { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.ContentPageSearch");
        /// <summary>
        ///  Getting Content page Close icon image url from Sitecore settings based on Site
        ///  /Site Settings/Site Specific Images/Content/ContentPageClose
        /// </summary>
        public static string ContentPageClose { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.ContentPageClose");
        /// <summary>
        /// Getting Header item path /Site Content/Navigation/Header
        /// </summary>
        public static string NavigationHeaderPath { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.NavigationHeaderPath");
        /// <summary>
        /// Getting footer item path Site Content/Navigation/Footer
        /// </summary>
        public static string NavigationFooterPath { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.NavigationFooterPath");
        /// <summary>
        /// Getting Image location path "Assets/Project/Emaarcommunities/images/"
        /// </summary>
        public static string ImageLocatation { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.ImageLocation");
        /// <summary>
        /// Getting Image location path "~/Assets/Project/Emaarcommunities/images/icon/"
        /// </summary>
        public static string IconRootPath { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.IconRootPath");

        /// <summary>
        /// Getting date format
        /// </summary>
        public static string DateFormat { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.DateFormat");
        /// <summary>
        ///  NewsThumnailPixel
        /// </summary>
        public static string NewsThumnailPixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.NewsThumnailPixel");
        /// <summary>
        ///  DownloadThumnailPixel
        /// </summary>
        public static string DownloadThumnailPixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.DownloadThumnailPixel");
        /// <summary>
        ///  AlbumThumnailPixel
        /// </summary>
        public static string AlbumThumnailPixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.AlbumThumnailPixel");
        /// <summary>
        ///  HeroImage image pixel
        /// </summary>
        public static string HeroImagepixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.HeroImagepixel");
        /// <summary> 
        ///  HomePageCarousel image pixel
        /// </summary>
        public static string HomePageCarouselpixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.HomePageCarouselpixel");
        /// <summary>
        ///  ImageText image pixel
        /// </summary>
        public static string ImageTextpixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.ImageTextpixel");
        /// <summary>
        ///  Parallax image pixel
        /// </summary>
        public static string Parallaxpixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Parallaxpixel");
        /// <summary>
        ///  ContentPageBannerpixel image pixel
        /// </summary>
        public static string ContentPageBannerpixel { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.ContentPageBannerpixel");
        /// <summary>
        ///  RelatedPages image pixel
        /// </summary>
        public static string RelatedPages { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.RelatedPages");
        /// <summary>
        ///  This name is used to identify the Index based on Communities Sites
        /// </summary>
        public static string SearchIndexCommunitiesName { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.SearchIndexCommunitiesName");
        /// <summary>
        ///  This setting is used to identify for which sites all communities related Pipeline/Processors should execute
        /// </summary>
        public static string CommunitiesPipelinesExecutableWebsites { get; set; } = GetSetting("EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.CommunitiesPipelinesExecutableWebsites");
    }
}