using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types.Mediacenter;
using EMAAR.Emaarcommunities.Foundation.Search.Models;
using Sitecore.Data.Items;

namespace EMAAR.Emaarcommunities.Feature.Listing.Interface
{
    /// <summary>
    /// This class is used to get the model to display view with all top 3 News/Downloads/events/Image albums/Video albums
    /// </summary>
    public interface IMediacenterViewmodel
    {
        /// <summary>
        /// Media center context item details
        /// </summary>
         IMediacenter Mediacenter { get; set; }
        /// <summary>
        /// Search results of Image Albums
        /// </summary>
         SearchResultsGeneric<ListingSearchResultItem> ImageAlbumItems { get; set; }
        /// <summary>
        /// Search results of Video Albums
        /// </summary>
         SearchResultsGeneric<ListingSearchResultItem> VideoAlbumItems { get; set; }
        /// <summary>
        /// Verifying whether it is Video gallery or Video album
        /// </summary>
         bool IsVideoAlbum { get; set; }
        /// <summary>
        /// Verifying whether it is image gallery or image album
        /// </summary>
         bool IsImageAlbum { get; set; }
        /// <summary>
        /// Search results of News 
        /// </summary>
         SearchResultsGeneric<ListingSearchResultItem> NewsItems { get; set; }
        /// <summary>
        /// Search results of Events
        /// </summary>
         SearchResultsGeneric<ListingSearchResultItem> EventsItems { get; set; }
        /// <summary>
        /// Search results of Downloads
        /// </summary>
         SearchResultsGeneric<ListingSearchResultItem> DownloadsItems { get; set; }
        /// <summary>
        ///Image Gallery Item in Mediacenter 
        /// </summary>
         Item ImageGalleryItem { get; set; }
        /// <summary>
        /// Image Album Item in Mediacenter 
        /// </summary>
         Item ImageAlbumItem { get; set; }
        /// <summary>
        /// Video Gallery Item in Mediacenter 
        /// </summary>
         Item VideoGalleryItem { get; set; }
        /// <summary>
        /// Video Album Without Filters/ Video Album With Filters Item in Mediacenter 
        /// </summary>
        Item VideoAlbumItem { get; set; }
        /// <summary>
        /// Video Album Without Filters Item in Mediacenter 
        /// </summary>
        Item VideoAlbumWithoutFiltersItem { get; set; }
        /// <summary>
        /// Video Album With Filters Item in Mediacenter 
        /// </summary>
        Item VideoAlbumWithFiltersItem { get; set; }
        /// <summary>
        /// Video  Item in Mediacenter 
        /// </summary>
        Item VideoItem { get; set; }
        /// <summary>
        /// News in Mediacenter 
        /// </summary>
         Item NewsListingPageItem { get; set; }
        /// <summary>
        /// Downloads Item in Mediacenter 
        /// </summary>
         Item DownloadPageItem { get; set; }
        /// <summary>
        /// Events Item in Mediacenter 
        /// </summary>
         Item EventsListingPageItem { get; set; }
    }
}
