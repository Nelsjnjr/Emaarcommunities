using System;
using EMAAR.Emaarcommunities.Foundation.Search.Helpers;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Framework.Helper;

namespace EMAAR.Emaarcommunities.Foundation.Search.ComputedFields
{
    public class ThumbnailUrl : IComputedIndexField
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
        /// Computed field for thumbnailimage url
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

                if (formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageItemTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoItemTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.DownloadItemTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageAlbumPageTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoAlbumWithFiltersTemplateID)) ||
                    formattedTemplateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoAlbumWithoutFiltersTemplateID)))
                {

                    string[] pixels = null;
                    ImageField imageField = null;
                    switch (SearchHelper.FormatGuid(item.TemplateID.ToString()))
                    {
                        //For news thumbnail
                        case var templateId when templateId.Equals(SearchHelper.FormatGuid(CommonConstants.NewsTemplateID)):
                            imageField = item.Fields["Banner"];
                            if(imageField.MediaID.IsNull)
                            {
                                imageField = item.Database.GetItem(ID.Parse(CommonConstants.NoImageItemID)).Fields[CommonConstants.Image];
                            }
                            pixels = SitecoreSettings.NewsThumnailPixel.ToLower().Split('x');
                            return AdvancedImageHelper.GetImageFieldUrl(imageField, System.Convert.ToInt32(pixels[0]), System.Convert.ToInt32(pixels[1])).Replace("/sitecore/shell", "");
                        //For downloads thumbnail
                        case var templateId when templateId.Equals(SearchHelper.FormatGuid(CommonConstants.DownloadItemTemplateID)):
                            imageField = item.Fields["Image"];
                            if (imageField.MediaID.IsNull)
                            {
                                imageField = item.Database.GetItem(ID.Parse(CommonConstants.NoImageItemID)).Fields[CommonConstants.Image];
                            }
                            pixels = SitecoreSettings.DownloadThumnailPixel.ToLower().Split('x');
                            return AdvancedImageHelper.GetImageFieldUrl(imageField, System.Convert.ToInt32(pixels[0]), System.Convert.ToInt32(pixels[1])).Replace("/sitecore/shell", "");
                        //For image and video album thumbnail
                        case var templateId when templateId.Equals(SearchHelper.FormatGuid(CommonConstants.ImageAlbumPageTemplateID)) ||
                            templateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoAlbumWithFiltersTemplateID)) ||
                            templateId.Equals(SearchHelper.FormatGuid(CommonConstants.VideoAlbumWithoutFiltersTemplateID)):
                            imageField = item.Fields["ThumbnailImage"];
                            if (imageField.MediaID.IsNull)
                            {
                                imageField = item.Database.GetItem(ID.Parse(CommonConstants.NoImageItemID)).Fields[CommonConstants.Image];
                            }
                            pixels = SitecoreSettings.AlbumThumnailPixel.ToLower().Split('x');
                            return AdvancedImageHelper.GetImageFieldUrl(imageField, System.Convert.ToInt32(pixels[0]), System.Convert.ToInt32(pixels[1])).Replace("/sitecore/shell", "");
                        // This is for Video item and image item thumbnail
                        default:
                            imageField = item.Fields["Image"];
                            pixels = SitecoreSettings.AlbumThumnailPixel.ToLower().Split('x');
                            return AdvancedImageHelper.GetImageFieldUrl(imageField, System.Convert.ToInt32(pixels[0]), System.Convert.ToInt32(pixels[1])).Replace("/sitecore/shell", "");
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

            return null;
        }

        #endregion


    }
}