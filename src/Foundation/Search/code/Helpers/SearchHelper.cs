using EMAAR.Emaarcommunities.Foundation.Search.Models;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Sites;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
namespace EMAAR.Emaarcommunities.Foundation.Search.Helpers
{
    /// <summary>
    /// SearchHelper class definition.
    /// </summary>

    public static class SearchHelper
    {
        /// <summary>
        /// Generic method to create predicate
        /// </summary>
        /// <param name="searchFields">List of Search Fields.</param>
        /// <returns>predicate.</returns>        
        public static Expression<Func<T, bool>> BuildPredicate<T>(List<SearchCondition> searchFields) where T : ListingSearchResultItem
        {
            Expression<Func<T, bool>> predicate = PredicateBuilder.True<T>();
            if (searchFields != null && searchFields.Any())
            {
                foreach (SearchCondition searchField in searchFields)
                {
                    if (!string.IsNullOrWhiteSpace(searchField.Name))
                    {
                        Expression<Func<T, bool>> innerPredicate = PredicateBuilder.False<T>();
                        if (!string.IsNullOrWhiteSpace(searchField.Value))
                        {
                            foreach (string val in searchField.Value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (searchField.CompareType == CompareType.ExactMatch && !string.IsNullOrWhiteSpace(searchField.Value))
                                {
                                    innerPredicate = innerPredicate.Or(item => item[searchField.Name].Equals(val));
                                }
                                else if (searchField.CompareType == CompareType.PartialMatch && !string.IsNullOrWhiteSpace(searchField.Value))
                                {
                                    innerPredicate = innerPredicate.Or(item => item[searchField.Name].Contains(val));
                                }
                                else if (searchField.CompareType == CompareType.NotEquals && !string.IsNullOrWhiteSpace(searchField.Value))
                                {
                                    innerPredicate = innerPredicate.Or(item => !item[searchField.Name].Equals(val));
                                }
                                else if (searchField.CompareType == CompareType.StartsWith && !string.IsNullOrWhiteSpace(searchField.Value))
                                {
                                    innerPredicate = innerPredicate.Or(item => item[searchField.Name].StartsWith(val));
                                }
                                else if (searchField.CompareType == CompareType.GreaterOrEqual && !string.IsNullOrWhiteSpace(searchField.Value))
                                {
                                    innerPredicate = innerPredicate.Or(item => item.date_dt >= DateTime.Parse(val));

                                }

                                else if (searchField.CompareType == CompareType.LessThan && !string.IsNullOrWhiteSpace(searchField.Value))
                                {
                                    innerPredicate = innerPredicate.Or(item => item.date_dt < DateTime.Parse(val));
                                }

                            }
                        }
                        else
                        {
                            if (searchField.CompareType == CompareType.IsNotNull)
                            {
                                innerPredicate = innerPredicate.Or(item => item[searchField.Name] != null);

                            }
                            else if (searchField.CompareType == CompareType.IsNotEmpty)
                            {
                                innerPredicate = innerPredicate.Or(item => item[searchField.Name] != "");

                            }
                        }

                        if(searchField.AddORcondition)
                            predicate = predicate.Or<T>(innerPredicate);
                        else
                            predicate = predicate.And<T>(innerPredicate);
                    }

                }

            }
            return predicate;

        }

        /// <summary>
        /// Add basic search conditions.
        /// </summary>
        /// <param name="searchConditions">searchConditions.</param>               
        /// <returns>List of search conditions.</returns>  
        public static List<SearchCondition> AddBasicSearchConditions(List<SearchCondition> searchConditions)
        {
            searchConditions.Add(new SearchCondition() { Name = CommonConstants.Language, Value = Sitecore.Context.Language.Name.ToLower() });
            searchConditions.Add(new SearchCondition() { Name = CommonConstants.Latestversion, Value = "1" });
            searchConditions.Add(new SearchCondition() { Name = CommonConstants.CustomSiteFieldName, Value = SiteContext.Current.Name.ToLower() });
            return searchConditions;

        }

        /// <summary>
        /// Get Search Index .
        /// </summary>            
        /// <returns>searh index.</returns>
        public static ISearchIndex GetIndex()
        {
            if (Context.Site != null && Context.Database != null && Context.Site.ContentStartPath.ToLower().Contains(String.Format("/{0}",SitecoreSettings.SearchIndexCommunitiesName)))
            {

                return ContentSearchManager.GetIndex(string.Format(SitecoreSettings.SearchIndexCommunitiesName + "_{0}_index", Context.Database.Name).ToLower());
            }
            else
            {
                return ContentSearchManager.GetIndex(string.Format(CommonConstants.DefaultIndexNamePrefix + "_{0}_index", Context.Database.Name).ToLower());
            }
        }




        /// <summary>
        /// Method to format guid - it removes hyphen, braces from th einput string and convert it to lower case
        /// </summary>
        /// <param name="unformattedGuid"></param>
        /// <returns></returns>
        public static string FormatGuid(string unformattedGuid)
        {
            string formattedGuid = string.Empty;
            string[] charactersTobeRemoved = { "-", "{", "}" };
            if (!string.IsNullOrWhiteSpace(unformattedGuid))
            {
                foreach (string character in charactersTobeRemoved)
                {
                    unformattedGuid = unformattedGuid.Replace(character, "");
                }
                formattedGuid = unformattedGuid.Trim().ToLower();
            }
            return formattedGuid;
        }


        /// <summary>
        /// Method to get youtube embed URL
        /// </summary>
        /// <param name="linkUrl"></param>
        /// <returns>embed url</returns>
        public static string GetYoutubeEmbedUrl(string linkUrl)
        {
            string youtubeId = string.Empty;
            string youtubeUrl = string.Empty;

            if (!string.IsNullOrWhiteSpace(linkUrl))
            {
                youtubeId = GetYoutubeId(linkUrl);
                if (!string.IsNullOrWhiteSpace(youtubeId))
                {
                    youtubeUrl = string.Format("https://www.youtube.com/embed/{0}?rel=0", youtubeId);
                }
            }
            return youtubeUrl;
        }


        /// <summary>
        /// Method to get youtube embed URL
        /// </summary>
        /// <param name="youTubeUrl"></param>
        /// <returns>embed url</returns>
        public static string GetYoutubeId(string youTubeUrl)
        {
            Match youtubeMatch =
           new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)")
           .Match(youTubeUrl);
            return youtubeMatch.Success ? youtubeMatch.Groups[1].Value : string.Empty;
        }


        /// <summary>
        /// Method to get image url
        /// </summary>
        /// <param name="unformattedGuid"></param>
        /// <returns></returns>
        public static string GetImageSource(Item item, ID imageFieldId)
        {
            if (item != null && !imageFieldId.IsNull)
            {
                ImageField imageField = item.Fields[imageFieldId];
                if (imageField != null && imageField.MediaItem != null)
                {
                    MediaUrlOptions mediaUrlOption = new MediaUrlOptions
                    {
                        Language = item.Language
                    };
                    string imageUrl = MediaManager.GetMediaUrl(imageField.MediaItem, mediaUrlOption);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        imageUrl = imageUrl.Replace("/sitecore/shell", "");
                    }

                    return imageUrl;
                }
            }

            return null;
        }

        /// <summary>
        /// Method to get current site info
        /// </summary>
        /// <param name="unformattedGuid"></param>
        /// <returns>SiteInfo</returns>
        public static SiteInfo GetSiteInfo(this Item item)
        {

            string itemPath = item.Paths.FullPath;
            SiteInfo site = SiteContextFactory.Sites
                .Where(s => s.RootPath != "" && itemPath.StartsWith(s.RootPath, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(s => s.RootPath.Length)
                .FirstOrDefault();

            return site;
        }


        /// <summary>
        /// Method to search conditions
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="conditions">conditions</param>        
        /// <returns>List<SearchCondition></returns>
        public static List<SearchCondition> AddFilterConditions(string filter, List<SearchCondition> conditions)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                List<string> filters = filter.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (filters != null && filters.Any())
                {
                    foreach (string filterString in filters)
                    {
                        List<string> filterParam = filterString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (filterParam != null && filterParam.Any())
                        {
                            if (filterParam[1].Equals(CommonConstants.AllValue) || filterParam[1].Equals(CommonConstants.AllEvents))
                            {
                                continue;
                            }
                            else if (filterParam[0].ToLower() == CommonConstants.EventType.ToLower() && filterParam[1].ToLower() == CommonConstants.Past.ToLower())
                            {
                                conditions.Add(new SearchCondition() { Name = CommonConstants.Date, Value = DateTime.UtcNow.ToString(), CompareType = CompareType.LessThan });
                            }
                            else if (filterParam[0].ToLower() == CommonConstants.EventType.ToLower() && filterParam[1].ToLower() == CommonConstants.Upcoming.ToLower())
                            {
                                conditions.Add(new SearchCondition() { Name = CommonConstants.Date, Value = DateTime.UtcNow.ToString(), CompareType = CompareType.GreaterOrEqual });
                            }
                            else
                            {
                                conditions.Add(new SearchCondition() { Name = filterParam[0], Value = filterParam[1], CompareType = CompareType.ExactMatch });
                            }
                        }
                    }

                }
            }
            return conditions;
        }

        /// <summary>
        /// Method to get url from item
        /// </summary>
        /// <param name="unformattedGuid"></param>
        /// <returns></returns>
        public static string GetURL(Item item)
        {
            if (item.Paths.IsMediaItem)
            {
                MediaUrlOptions mediaUrlOption = new MediaUrlOptions
                {
                    Language = item.Language
                };
                string mediaURL = MediaManager.GetMediaUrl(item, mediaUrlOption);
                if (!string.IsNullOrEmpty(mediaURL))
                {
                    return mediaURL.Replace("/sitecore/shell", "");
                }
            }

            UrlOptions urlOption = new UrlOptions
            {
                Language = item.Language
            };

            string link = LinkManager.GetItemUrl(item, urlOption);
            SiteInfo siteInfo = SearchHelper.GetSiteInfo(item);

            if (siteInfo == null)
            {
                return link;
            }

            string currentSiteStartItemPath = siteInfo.RootPath + siteInfo.StartItem;
            if (!string.IsNullOrWhiteSpace(link))
            {
                if (link.Contains("/content/"))
                {
                    return link.Replace("/shell", string.Empty).Replace(currentSiteStartItemPath, string.Empty);
                }
                else
                {
                    currentSiteStartItemPath = currentSiteStartItemPath.Replace("/content", string.Empty);
                    return link.Replace("/shell", string.Empty).Replace(currentSiteStartItemPath, string.Empty);
                }

            }
            return null;
        }

    }
}