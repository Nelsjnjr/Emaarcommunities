using System;
using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Framework.Helper;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class ImageUrl : IComputedIndexField
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
        /// Computed field for image url
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
                string imageUrl = string.Empty;
                if (formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageItemTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoItemTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.DownloadItemTemplateID)))
                {

                    ImageField imageField = item.Fields["Image"];
                    if (SearchHelper.FormatGuid(item.TemplateID.ToString()).Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)))
                    {
                        
                        imageField = item.Fields["Banner"];
                    }

                                    
                    return AdvancedImageHelper.GetImageFieldUrl(imageField, 0, 0).Replace("/sitecore/shell", ""); ;
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

            return null;
        }

        #endregion


    }
}