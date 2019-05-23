using EMAAR.Emaarcommunities.Feature.Listing.Interfaces;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types;
using Newtonsoft.Json;
using System.Web.Mvc;
using static EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings.SitecoreSettings;

namespace EMAAR.Emaarcommunities.Feature.Listing.Controllers
{
    /// <summary>
    /// ListingController class definition.
    /// </summary>

    public class ListingController : Controller
    {
        #region Private property


        private readonly IListingRepository _repo;

        #endregion

        #region constructor

        /// <summary>
        /// Listing Controller constructor
        /// </summary>        
        public ListingController(IListingRepository repo)
        {
            _repo = repo;
        }
        #endregion
        /// <summary>
        /// Method to get  items from index(like albums,news,downloads,events based on template and path )
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>     
        /// <param name="parentItemId">Parent id when directly access Folder item</param>       
        /// <returns>json string.</returns>   
        public string GetListingJSON(int pageNumber = -1, int pageSize = -1, string filter = "", string itemId = "", string listItemTemplateId = "", bool showFilters = false, string parentItemId = "")
        {
            return JsonConvert.SerializeObject(_repo.GetListingModel(pageNumber, pageSize, filter, itemId, listItemTemplateId, showFilters,parentItemId), Formatting.None);

        }


        /// <summary>
        /// Method to get search results json
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        public string GetSearchJSON(int pageNumber = -1, int pageSize = -1, string searchTerm = "*")
        {
            return JsonConvert.SerializeObject(_repo.GetSearchResultsModelObject(pageNumber, pageSize, searchTerm), Formatting.None);

        }

        #region Image Gallery

        /// <summary>
        /// Method to get image albums
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        [HttpGet]
        public ActionResult ImageGallery()
        {
            IImage_Gallery_Page imageGallery = _repo.GetImageGallery();
            return View($"{ViewPath}Listing/Images/ImageGallery.cshtml", imageGallery);
        }


        #endregion

        #region Image Album

        /// <summary>
        /// Method to get view of Album images
        /// </summary>    
        [HttpGet]
        public ActionResult ImagesAlbum()
        {
            IImage_Album imageAlbum = _repo.GetImageAlbum();
            return View($"{ViewPath}Listing/Images/ImageAlbum.cshtml", imageAlbum);
        }
        #endregion


        #region Video Gallery

        /// <summary>
        /// Method to get video albums
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        [HttpGet]
        public ActionResult VideoGallery()
        {
            IVideo_Gallery_Page videoGallery = _repo.GetVideoGalleryModel();
            return View($"{ViewPath}Listing/Videos/VideoGallery.cshtml", videoGallery);
        }

        #endregion



        #region Image Album

        /// <summary>
        /// Method to get view of Album videos
        /// </summary>    
        [HttpGet]
        public ActionResult VideosAlbum()
        {
            IVideo_Album_Without_Filters videoAlbum = _repo.GetVideoAlbumsModel(out bool ShowFilters);
            ViewBag.Showfilter = ShowFilters;
            return View($"{ViewPath}Listing/Videos/VideoAlbum.cshtml", videoAlbum);
        }


        #endregion


        #region News

        /// <summary>
        /// Method to get News
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        [HttpGet]
        public ActionResult NewsListing()
        {
            INews_Listing_Page newsListing = _repo.GetNewsListingPageModel();
            return View($"{ViewPath}Listing/News/NewsListing.cshtml", newsListing);
        }

        #endregion

        #region Events

        /// <summary>
        /// Method to get Events
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        [HttpGet]
        public ActionResult EventsListing()
        {
            IEvents_Listing_Page eventsListing = _repo.GetEventsListingPageModel(out string Year);
            ViewBag.Year = Year;
            return View($"{ViewPath}Listing/Events/EventsListing.cshtml", eventsListing);
        }

        #endregion

        #region Downloads

        /// <summary>
        /// Method to get downloads
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        [HttpGet]
        public ActionResult DownloadsListing()
        {
            IDownloads_Page downloadsListing = _repo.GetDownloadsListingPageModel();
            return View($"{ViewPath}Listing/Downloads/DownloadsListing.cshtml", downloadsListing);
        }

        #endregion

        #region Search

        /// <summary>
        /// Method to get Search Results
        /// </summary>
        /// <param name="pageNumber">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="filter">filter</param>          
        /// <returns>json string.</returns>   
        [HttpGet]
        public ActionResult SearchResults()
        {          
            return View($"{ViewPath}Listing/Search/SearchResults.cshtml", _repo.GetSearchPageModel());
        }

        #endregion
    }
}