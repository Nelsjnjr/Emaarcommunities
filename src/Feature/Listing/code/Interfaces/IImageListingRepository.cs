using EMAAR.ECM.Foundation.ORM.Models.sitecore.templates.Project.ECM.Page_Types;
using EMAAR.ECM.Foundation.Search.Models;

namespace EMAAR.ECM.Feature.Listing.Interfaces
{
    public interface IImageListingRepository
    {

        SearchResultsGeneric<ListingSearchResultItem> GetAlbums(int pageNumber = -1, int pageSize = -1, string filter = "", string itemId = "");


        SearchResultsGeneric<ListingSearchResultItem> GetAlbumImages(int pageNumber = -1, int pageSize = -1, string filter = "", string itemId = "");

        IImage_Gallery_Page GetImageGallery();

        IImage_Album GetImageAlbum();


    }
}
