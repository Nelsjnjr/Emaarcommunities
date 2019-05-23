using EMAAR.Emaarcommunities.Feature.Listing.Interface;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types.Mediacenter;
using EMAAR.Emaarcommunities.Foundation.Search.Models;
using Sitecore.Data.Items;

namespace EMAAR.Emaarcommunities.Feature.Listing.Models
{
    /// <summary>
    /// This class is used to get the model to display view with all top 3 News/Downloads/events/Image albums/Video albums
    /// </summary>
    [Service(typeof(IMediacenterViewmodel))]
    public class MediacenterViewmodel : IMediacenterViewmodel
    {
        /// <summary>
        /// Media center context item details
        /// </summary>
        public IMediacenter Mediacenter { get; set; }
        /// <summary>
        /// Search results of Image Albums
        /// </summary>
        public SearchResultsGeneric<ListingSearchResultItem> ImageAlbumItems { get; set; }
        /// <summary>
        /// Search results of Video Albums
        /// </summary>
        public SearchResultsGeneric<ListingSearchResultItem> VideoAlbumItems { get; set; }
        /// <summary>
        /// Verifying whether it is Video gallery or Video album
        /// </summary>
        public bool IsVideoAlbum { get; set; }
        /// <summary>
        /// Verifying whether it is image gallery or image album
        /// </summary>
        public bool IsImageAlbum { get; set; }
        /// <summary>
        /// Search results of News 
        /// </summary>
        public SearchResultsGeneric<ListingSearchResultItem> NewsItems { get; set; }
        /// <summary>
        /// Search results of Events
        /// </summary>
        public SearchResultsGeneric<ListingSearchResultItem> EventsItems { get; set; }
        /// <summary>
        /// Search results of Downloads
        /// </summary>
        public SearchResultsGeneric<ListingSearchResultItem> DownloadsItems { get; set; }
        /// <summary>
        ///Image Gallery Item in Mediacenter 
        /// </summary>
        public Item ImageGalleryItem { get; set; }
        /// <summary>
        /// Image Album Item in Mediacenter 
        /// </summary>
        public Item ImageAlbumItem { get; set; }
        /// <summary>
        /// Video Gallery Item in Mediacenter 
        /// </summary>
        public Item VideoGalleryItem { get; set; }
        /// <summary>
        /// Video Album Without Filters/ Video Album With Filters Item in Mediacenter 
        /// </summary>
        public Item VideoAlbumItem { get; set; }
        /// <summary>
        /// Video Album Without Filters Item in Mediacenter 
        /// </summary>
        public Item VideoAlbumWithoutFiltersItem { get; set; }
        /// <summary>
        /// Video Album With Filters Item in Mediacenter 
        /// </summary>
        public Item VideoAlbumWithFiltersItem { get; set; }
        /// <summary>
        /// Video  Item in Mediacenter 
        /// </summary>
        public Item VideoItem { get; set; }
        /// <summary>
        /// News in Mediacenter 
        /// </summary>
        public Item NewsListingPageItem { get; set; }
        /// <summary>
        /// Downloads Item in Mediacenter 
        /// </summary>
        public Item DownloadPageItem { get; set; }
        /// <summary>
        /// Events Item in Mediacenter 
        /// </summary>

        public Item EventsListingPageItem { get; set; }

    }
}