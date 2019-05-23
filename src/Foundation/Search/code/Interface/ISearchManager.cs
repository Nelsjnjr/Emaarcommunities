using EMAAR.Emaarcommunities.Foundation.Search.Models;
using Sitecore.ContentSearch.SearchTypes;
using System.Collections.Generic;

namespace EMAAR.Emaarcommunities.Foundation.Search.Interfaces
{
    /// <summary>
    /// Contract of generic search methods
    /// </summary>
    public interface ISearchManager
    {
        /// <summary>
        /// Generic Search Method 
        /// </summary>
        /// <param name="searchConditions">search conditions.</param>
        /// <param name="pageNo">Page Number.</param>
        /// <param name="pageSize">Page Size.</param>
        /// <param name="facetFields">facets</param>
        /// <param name="sortOptions">sort options</param>
        /// <returns>Search Results of Generic Type</returns>  
        SearchResultsGeneric<T> GetSearchResults<T>(List<SearchCondition> searchConditions, List<Facet> facetFields= null, SortOption sortOption = null, int pageNo = -1, int pageSize = -1, bool sortByYearAndOrder = false, bool sortByDateAndOrder = false, bool sortByDateAscAndOrder = false) where T : ListingSearchResultItem;
    }
}
