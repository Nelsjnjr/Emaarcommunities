using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using System;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class PageUrl : IComputedIndexField
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
        /// Computed field for page url
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


                // Events - Get URL from Navigation URL field if available
                if (SearchHelper.FormatGuid(item.TemplateID.ToString()).Equals(SearchHelper.FormatGuid(CommonConstants.EventsTemplateID))
                    && (CheckboxField)item.Fields[CommonConstants.IsEventsPage] != null)
                {
                    // Navigation URL field wil have prioroty
                    if (item.Fields[CommonConstants.NavigationURL] != null && !string.IsNullOrWhiteSpace(item.Fields[CommonConstants.NavigationURL].Value))
                    {
                        LinkField navigationUrl = item.Fields[CommonConstants.NavigationURL];
                        if (navigationUrl != null)
                        {
                            if (!string.IsNullOrEmpty(navigationUrl.Url))
                            {
                                return navigationUrl.Url;
                            }
                            else if (navigationUrl.TargetItem != null)
                            {
                                return SearchHelper.GetURL(navigationUrl.TargetItem);
                            }
                        }

                    }
                    else if (((CheckboxField)item.Fields[CommonConstants.IsEventsPage]).Checked)
                    {
                        return SearchHelper.GetURL(item);
                    }
                    else
                    {
                        return null;
                    }

                }         
                // Downloads - Get URL from Download Link field if available
                if (SearchHelper.FormatGuid(item.TemplateID.ToString()).Equals(SearchHelper.FormatGuid(CommonConstants.DownloadItemTemplateID))
                    && item.Fields["Download Link"] != null && !string.IsNullOrWhiteSpace(item.Fields["Download Link"].Value))
                {
                    LinkField downloadLink = item.Fields["Download Link"];
                    if (downloadLink != null)
                    {
                        if (!string.IsNullOrEmpty(downloadLink.Url))
                        {
                            return downloadLink.Url;
                        }
                        else if (downloadLink.TargetItem != null)
                        {
                            return SearchHelper.GetURL(downloadLink.TargetItem);
                        }
                    }
                }
                // Video - Get Youtube URL
                if (SearchHelper.FormatGuid(item.TemplateID.ToString()).Equals(SearchHelper.FormatGuid(CommonConstants.VideoItemTemplateID)) && item.Fields["Video"]!=null)
                {
                    LinkField videoField = item.Fields["Video"];
                    if (videoField != null && !string.IsNullOrEmpty(videoField.Url))
                    {
                        string youtubeUrl = SearchHelper.GetYoutubeEmbedUrl(videoField.Url);
                        return youtubeUrl ?? null;                        
                    }
                }
                string currentLayoutXml = LayoutField.GetFieldValue(item.Fields[FieldIDs.LayoutField]);
                if (string.IsNullOrEmpty(currentLayoutXml))  return null;

                LayoutDefinition layout = LayoutDefinition.Parse(currentLayoutXml);
                if (layout.Devices.Count >= 1)
                    return SearchHelper.GetURL(item);
       

            }


            catch (Exception ex)
            {
                string itemId = string.Empty;
                if (item != null)
                    itemId = item.ID.ToString();
                Sitecore.Diagnostics.Log.Error(this.GetType().Name + " - Item ID: " + itemId, ex, this);

            }

            return null;
        }

   

        #endregion

      
    }
}