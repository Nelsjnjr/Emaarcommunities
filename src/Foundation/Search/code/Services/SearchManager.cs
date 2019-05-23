using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using EMAAR.Emaarcommunities.Foundation.Search.Interfaces;
using EMAAR.Emaarcommunities.Foundation.Search.Models;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;

namespace EMAAR.Emaarcommunities.Foundation.Search.Services
{
    /// <summary>
    /// SearchManager class definition.
    /// </summary>

    [Service(typeof(ISearchManager))]
    public class SearchManager : ISearchManager
    {
        /// <summary>
        /// Generic Search Method 
        /// </summary>
        /// <param name="searchConditions">search conditions.</param>
        /// <param name="pageNo">Page Number.</param>
        /// <param name="pageSize">Page Size.</param>
        /// <returns>Search Results of Generic Type</returns>        
        public SearchResultsGeneric<T> GetSearchResults<T>(List<SearchCondition> searchConditions, List<Facet> facetFields = null, SortOption sortOption = null, int pageNo = -1, int pageSize = -1, bool sortByYearAndOrder = false, bool sortByDateAndOrder = false, bool sortByDateAscAndOrder = false) where T : ListingSearchResultItem
        {
            searchConditions = SearchHelper.AddBasicSearchConditions(searchConditions);
            IProviderSearchContext searchContext = SearchHelper.GetIndex().CreateSearchContext();
            Expression<Func<T, bool>> predicate = SearchHelper.BuildPredicate<T>(searchConditions);
            IQueryable<T> searchQuery = null;

            if (sortByYearAndOrder)
            {
                searchQuery = searchContext.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)).Where(predicate).OrderBy(x => x[CommonConstants.CustomSortorder]).ThenByDescending(x => x[CommonConstants.YearFacetField]);
            }
            else if (sortByDateAscAndOrder)
            {
                searchQuery = searchContext.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)).Where(predicate).OrderBy(x => x[CommonConstants.CustomSortorder]).ThenBy(x => x[CommonConstants.DateField]);
            }
            else if (sortByDateAndOrder)
            {
                searchQuery = searchContext.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)).Where(predicate).OrderBy(x => x[CommonConstants.CustomSortorder]).ThenByDescending(x => x[CommonConstants.DateField]);
            }

            else if (sortOption != null)
            {
                // Pass CultureExecutionContext object to pick index analyzer for query
                if (sortOption.SortOrder.Equals(SortOrder.Descending))
                {
                    searchQuery = searchContext.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)).Where(predicate).OrderByDescending(x => x[sortOption.SortFieldName]);
                }
                else
                {
                    searchQuery = searchContext.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)).Where(predicate).OrderBy(x => x[sortOption.SortFieldName]);
                }
            }
            else
            {
                searchQuery = searchContext.GetQueryable<T>(new CultureExecutionContext(Sitecore.Context.Language.CultureInfo)).Where(predicate);
            }

            if (facetFields != null && facetFields.Any())
            {
                // Add facets to the search query
                foreach (Facet facet in facetFields)
                {
                    searchQuery = searchQuery.FacetOn(f => f[facet.facetField], facet.minCount);
                }
            }

            FacetResults facets = searchQuery.GetFacets();
            SearchResultsGeneric<T> searchResults = new SearchResultsGeneric<T>();

            if (pageNo == -1 && pageSize == -1)
            {
                searchResults.results = new Results<T>() { results = searchQuery.ToList(), Totalcount = searchQuery.GetResults().Count() };
            }
            else
            {
                searchResults.results = new Results<T>() { results = searchQuery.Page(pageNo, pageSize).ToList(), Totalcount = searchQuery.GetResults().Count() };
            }
            List<string> itemIdsList = new List<string>();
            if (facets != null && facets.Categories != null && facets.Categories.Any())
            {
                List<Filters> filters = new List<Filters>();
                foreach (FacetCategory category in facets.Categories)
                {
                    List<FilterValues> filterValuesList = new List<FilterValues>();
                    foreach (FacetValue val in category.Values)
                    {
                        bool parse = Guid.TryParse(val.Name, out Guid id);
                        if (parse && id != null)
                        {
                            itemIdsList.Add(val.Name);
                        }

                        filterValuesList.Add(new FilterValues() { id = val.Name, label = val.Name });
                    }


                    filters.Add(new Filters() { filterLabel = category.Name, filterValues = filterValuesList });
                }



                Dictionary<string, string> itemIdsWithValues = GetItemIdValues(itemIdsList);

                foreach (Filters filter in filters)
                {
                    foreach (FilterValues filterVal in filter.filterValues)
                    {
                        if (!string.IsNullOrWhiteSpace(itemIdsWithValues.SingleOrDefault(x => x.Key.Equals(filterVal.label)).Value))
                        {
                            filter.filterValues.SingleOrDefault(x => x.label.Equals(filterVal.label)).label = itemIdsWithValues.SingleOrDefault(x => x.Key.Equals(filterVal.label)).Value;
                        }
                    }

                    if (filter.filterLabel.Equals(CommonConstants.YearFacetField))
                    {
                        filter.filterValues = filter.filterValues.OrderByDescending(x => Convert.ToInt32(x.label)).ToList();
                    }
                    else
                    {
                        filter.filterValues = filter.filterValues.OrderBy(x => x.label).ToList();
                    }


                    if (facets.Categories.Any(x => x.Name.ToLower().Equals(filter.filterLabel.ToLower())))
                    {
                        string label = facetFields.SingleOrDefault(x => (x.facetField.Equals(facets.Categories.SingleOrDefault(y => y.Name.ToLower().Equals(filter.filterLabel.ToLower())).Name))).allLabel;
                        filter.filterValues.Insert(0, new FilterValues() { id = CommonConstants.AllValue, label = label });
                    }

                }
                //Default filters
                searchResults.filters = filters;

            }

            return searchResults;

        }


        /// <summary>
        /// Get values for list of ids.
        /// </summary>
        /// <param name="itemIds">item ids</param>        
        /// <returns>dictionary.</returns>
        public Dictionary<string, string> GetItemIdValues(List<string> allItemIds)
        {
            Dictionary<string, string> itemIdsWithValues = new Dictionary<string, string>();

            //Making batch wise calls to get search results for Ids
            int batch = 0;
            while (CommonConstants.BatchSize * batch <= allItemIds.Count)
            {
                List<string> itemIds = allItemIds.Skip(CommonConstants.BatchSize * batch).Take(CommonConstants.BatchSize).ToList();
                List<SearchCondition> searchFilters = new List<SearchCondition>();
                List<string> itemIdsFormatted = new List<string>();
                foreach (string id in itemIds)
                {
                    if (string.IsNullOrEmpty(id))
                    {
                        continue;
                    }

                    if (id.Contains("-"))
                    {
                        itemIdsFormatted.Add(id.Substring(0, id.IndexOf("-")));
                    }

                    itemIdsFormatted.Add(id);
                }
                if (itemIdsFormatted.Count <= 0)
                {
                    return itemIdsWithValues;
                }

                searchFilters.Add(new SearchCondition() { Name = CommonConstants.IndexIdField, Value = string.Join(",", itemIdsFormatted) });

                SearchResultsGeneric<ListingSearchResultItem> resultsPayload = GetSearchResults<ListingSearchResultItem>(searchFilters);

                if (resultsPayload != null && resultsPayload.results.Totalcount > 0)
                {
                    foreach (ListingSearchResultItem result in resultsPayload.results.results)
                    {
                        string id = SearchHelper.FormatGuid(result.ItemId.ToString());
                        if (!itemIdsWithValues.ContainsKey(id))
                        {
                            if (itemIds.Any(x => x.Contains(id + "-")))
                            {
                                id = itemIds.Single(x => x.Contains(id + "-"));
                            }

                            if (!string.IsNullOrWhiteSpace(result.title) && !result.title.Equals(CommonConstants.NameStandardValueToken))
                            {
                                itemIdsWithValues.Add(id, result.title);
                            }
                            else if (!string.IsNullOrWhiteSpace(result.value))
                            {
                                itemIdsWithValues.Add(id, result.value);
                            }
                            else
                            {
                                itemIdsWithValues.Add(id, result.Name);
                            }
                        }

                    }

                    if (itemIds.Any(x => x.Equals(CommonConstants.InvalidID)))
                    {
                        itemIdsWithValues.Add(CommonConstants.InvalidID, CommonConstants.NoneValue);
                    }
                }
                batch++;
            }
            return itemIdsWithValues;

        }


    }
}