using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Web;
using System;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class ExternalUrl : IComputedIndexField
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
        /// Computed field for external url
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

                // Downloads external link
                if (SearchHelper.FormatGuid(item.TemplateID.ToString()).Equals(SearchHelper.FormatGuid(CommonConstants.DownloadItemTemplateID))
                    && item.Fields["External Link"] != null && !string.IsNullOrWhiteSpace(item.Fields["External Link"].Value))
                {
                    LinkField externalLink = item.Fields["External Link"];
                    if (externalLink != null)
                    {
                        if (!string.IsNullOrEmpty(externalLink.Url))
                        {
                            return externalLink.Url;
                        }
                        else if (externalLink.TargetItem != null)
                        {
                            return SearchHelper.GetURL(externalLink.TargetItem);
                        }
                    }
                }

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