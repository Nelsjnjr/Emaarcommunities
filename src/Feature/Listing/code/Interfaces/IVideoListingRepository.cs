using EMAAR.ECM.Foundation.ORM.Models.sitecore.templates.Project.ECM.Page_Types;
using EMAAR.ECM.Foundation.Search.Models;

namespace EMAAR.ECM.Feature.Listing.Interfaces
{
    public interface IVideoListingRepository
    {

        SearchResultsGeneric<ListingSearchResultItem> GetVideoAlbums(int pageNumber = -1, int pageSize = -1, string filter = "", string itemId = "");


        SearchResultsGeneric<ListingSearchResultItem> GetVideosOfAlbum(int pageNumber = -1, int pageSize = -1, string filter = "", string itemId = "");

        IVideo_Gallery_Page GetVideoGalleryModel();

        IVideo_Album GetVideoAlbumsModel();


    }
}
