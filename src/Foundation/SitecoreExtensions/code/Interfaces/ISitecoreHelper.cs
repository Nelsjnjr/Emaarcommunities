using EMAAR.Emaarcommunities.Foundation.ORM.Models;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using Sitecore.Data.Items;

namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Interfaces
{
    /// <summary>
    /// This SitecoreHelper interface used to retrieve common data from Sitecore
    /// </summary>
    public interface ISitecoreHelper
    {
        #region property

        string HomePageSearch { get; }
        /// <summary>
        ///  Getting Home page search icon image url from Sitecore settings based on Site
        /// </summary>
        string HomePageClose { get; }
        /// <summary>
        ///  Getting Content page Close icon image url from Sitecore settings based on Site
        /// </summary>
        string ContentPageSearch { get; }
        /// <summary>
        ///  Getting Content page Close icon image url from Sitecore settings based on Site
        /// </summary>
        string ContentPageClose { get; }

        /// <summary>
        /// Getting Header Navigation details
        /// </summary>
        IHeader NavigationHeader { get; }
        /// <summary>
        /// Getting footer navigation details
        /// </summary>
        IFooter NavigationFooter { get; }

        /// <summary>
        ///Image Gallery Item in Mediacenter 
        /// </summary>
        Item ImageGalleryItem { get; }
        /// <summary>
        /// Image Album Item in Mediacenter 
        /// </summary>
        Item ImageAlbumItem { get; }
        /// <summary>
        /// Video Gallery Item in Mediacenter 
        /// </summary>
        Item VideoGalleryItem { get; }
        /// <summary>
        /// Video Album Without Filter Item in Mediacenter 
        /// </summary>
        Item VideoAlbumWithoutFilterItem { get; }
        /// <summary>
        ///  Video Album With Filter Item in Mediacenter 
        /// </summary>
        Item VideoAlbumWithFilterItem { get; }
        /// <summary>
        /// Video Item in Mediacenter 
        /// </summary>
        Item VideoItem { get; }
        /// <summary>
        /// News in Mediacenter 
        /// </summary>
        Item NewsListingPageItem { get; }
        /// <summary>
        /// Downloads Item in Mediacenter 
        /// </summary>
        Item DownloadPageItem { get; }
        /// <summary>
        /// Events Item in Mediacenter 
        /// </summary>
        Item EventsListingPageItem { get; }
        /// <summary>
        /// Sitemap Datasource from Header navigation datasource
        /// </summary>
        IHeader Sitemap { get; }
      
        #endregion

    }
}
