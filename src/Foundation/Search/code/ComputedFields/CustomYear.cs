using System;
using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class CustomYear : IComputedIndexField
    {

        #region Properties
        public string FieldName
        {
            get; set;
        }

        public string ReturnType
        {
            get; set;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Computed field for year
        /// </summary>
        /// <param name="indexable">IIndexable</param>
        /// <returns>object.</returns>    
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = null;

            try
            {
                item = indexable as SitecoreIndexableItem;
                if (item == null)
                {
                    return null;
                }

                string formattedTemplateId = SearchHelper.FormatGuid(item.TemplateID.ToString());

                if (formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageAlbumPageTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageItemTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoAlbumWithoutFiltersTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoItemTemplateID)))
                {
                    string query = string.Empty;
                    switch (formattedTemplateId)
                    {
                        case var imageTemplateId when imageTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageAlbumPageTemplateID)) ||
                        imageTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageItemTemplateID)):
                            query = item.Paths.FullPath + "/ancestor-or-self::*[@@templateid='" + CommonConstants.ImageAlbumYearFolderTemplateID + "']";
                            break;
                        case var videoTemplateId when videoTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoAlbumWithoutFiltersTemplateID)) ||
                        videoTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoItemTemplateID)):
                            query = item.Paths.FullPath + "/ancestor-or-self::*[@@templateid='" + CommonConstants.VideoAlbumYearFolderTemplateID + "']";
                            break;

                        default:
                            break;
                    }                 
                    Item yearFolder = item.Database.SelectSingleItem(query);
                    if (yearFolder != null && int.TryParse(yearFolder.Name, out int year))
                    {
                         return year;
                    }
                }
                else if (formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.EventsTemplateID)))
                {
                    if (item.Fields["Date"] != null && !string.IsNullOrEmpty(item.Fields["Date"].Value))
                    {
                        DateField dt = item.Fields["Date"];
                        return dt.DateTime.Year;
                    }
                    else
                    {
                        string query = string.Empty;
                        switch (formattedTemplateId)
                        {
                            case var newsTemplateId when newsTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)):
                                query = item.Paths.FullPath + "/ancestor-or-self::*[@@templateid='" + CommonConstants.NewsYearFolderTemplateID + "']";
                                break;
                            case var eventsTemplateId when eventsTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.EventsTemplateID)):
                                query = item.Paths.FullPath + "/ancestor-or-self::*[@@templateid='" + CommonConstants.EventsYearFolderTemplateID + "']";
                                break;
                            default:
                                break;
                        }                      
                        Item yearFolder = item.Database.SelectSingleItem(query);
                        if (yearFolder != null && int.TryParse(yearFolder.Name, out int year))
                        {
                            return year;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string itemId = string.Empty;
                if (item != null)
                {
                    itemId = item.ID.ToString();
                }

                Sitecore.Diagnostics.Log.Error(GetType().Name + " - Item ID: " + itemId, ex, this);

            }

            return System.DateTime.Today.Year;
        }

        #endregion


    }
}